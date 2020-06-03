﻿
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class RealtimeTester:MonoBehaviour
{
    static RealtimeTester singleton;

    public BoolVarValue use;

    public float editorRunTestsEvery = 5;
    public float runtimeRunTestsEvery = 5;
    public float editorSearchSceneEvery = 30;
    public float runtimeSearchSceneEvery = 30;

    public List<Failure> failedTests;

    Coroutine testRunner, testableSearcher;
    public UnitySetups setups;

    [SerializeField] SceneSearchByClassType sceneSearch;

    MonoBehaviour[] SceneCache => sceneSearch.sceneCache;
    int[] Types => sceneSearch.foundTypes;
    // Only items from scene cache that are testable
    List<ITestable> TestableCache => sceneSearch.testableCache;

    static RealtimeTester Singleton {
        get {
            if (singleton == null) singleton = GameObject.FindObjectOfType<RealtimeTester>();
            if (singleton == null)
            {
                Debug.Log("[Singletons] RealtimeTester isnt in scene. Adding it manually.");
                singleton = new GameObject("[AutoGeneratedSingleton] Realtime tester").AddComponent<RealtimeTester>();
                singleton.GetComponent<RealtimeTester>().use.Value = true;
                singleton.GetComponent<RealtimeTester>().sceneSearch.use.Value = true;
            }
            return singleton;
        }
    }

    public void RunExternally()
    {
        EnsureLayersRun();
    }


    private void Start()
    {
        EnsureLayersRun();
    }

    private void Update()
    {
        EnsureLayersRun();
    }

    private void OnRenderObject()
    {
        EnsureLayersRun();
    }
    private void OnDestroy()
    {
        singleton = null;
    }
    void EnsureLayersRun()
    {
        if (failedTests == null) failedTests = new List<Failure>();
        if (sceneSearch == null) sceneSearch = new SceneSearchByClassType();
        if (testableSearcher == null)
            testableSearcher = sceneSearch.RunSearch(this);
        if(setups == null) setups = new UnitySetups();
        if (testRunner == null)
        {
            Debug.Log("Rerunning test runner.");
            testRunner = StartCoroutine(RunTestsAndSetups());
        }
    }
    internal static void Assert(bool assertTrue, MonoBehaviour unityClass, string message)
    {
        if (!assertTrue)
            Err(unityClass, message);
    }
    internal static void Assert(bool assertTrue, object source, string message)
    {
        if (!assertTrue)
            Err(source, message);
    }
    internal static void Err(object source, string v)
    {
        Singleton.AddFailure(new Failure(source, v));
    }

    internal static void AssertSceneReference(MonoBehaviour script, MonoBehaviour source)
    {
        if (!Application.isPlaying || Time.time < 1)
            Assert(script != null, source, "[1]Some script isn't assigned in inspector."+ source.GetType());
    }
    internal static void AssertSceneReference(MonoBehaviour script, MonoBehaviour source, string msg)
    {
        if (!Application.isPlaying || Time.time < 1)
            Assert(script != null, source, "[2]Some script isn't assigned in inspector. " + msg);
    }

    internal static void Err(MonoBehaviour source, string v)
    {
        Singleton.AddFailure(new Failure(source, v));
    }
    private void AddFailure(Failure failure)
    {
        failedTests.Add(failure);
    }

   
    private void ResetFailedTests()
    {
        failedTests.Clear();
    }

    public static void DestroyedTestableObject(ITestable it)
    {
        Singleton.TestableCache.Remove(it);
    }

    float GetSearchCycle()
    {
        if (!Application.isPlaying)
            return editorSearchSceneEvery;
        return runtimeSearchSceneEvery;
    }


    private IEnumerator RunTestsAndSetups()
    {
        yield return null; // wait 1 frame to skip start.
        while (true)
        {
            if (use == null)
            {
                use = new BoolVarValue();
                Debug.Log("Init Realtime tester values.");
            }
            if (!use.Value)
            {
                yield return new WaitForSeconds(1);
                continue;
            }

            RunTestsAndSetupsInSceneOnce();

            yield return new WaitForSeconds(GetTestRunCycle());
        }
    }

    [ContextMenu("Run tests")]
    private void RunTestsAndSetupsInSceneOnce()
    {
        ResetFailedTests();
        for (int i = 0; i < SceneCache.Length; i++)
        {
            int scriptType = Types[i];
            ITestable test = SceneCache[i] as ITestable;
            ISetupUnity setup = SceneCache[i] as ISetupUnity;
            if (test != null)
            {
                test.TestInitialState();
            } 
            if (setup != null)
            {
                if (!Application.isPlaying)
                {
                    if (!setups.RunSetup(setup))
                    {
                        Debug.LogErrorFormat(SceneCache[i], "[RealtimeSetup] Failed to setup{0}", SceneCache[i]);
                    }
                }
            }
        }
        for (int i = 0; i < 5 && i < failedTests.Count; i++)
        {
            if (failedTests[i].Mono)
            {
                Debug.LogErrorFormat(failedTests[i].Mono, "[RealtimeTester] Test Failures = {0}. [{2}]={1}", failedTests.Count, failedTests[i].Message, i);
            }
            else
            {
                Debug.LogErrorFormat("[RealtimeTester] Test Failures = {0} Object: {1}. [{3}]={2}", failedTests.Count, failedTests[i].AdditionalObject, failedTests[i].Message, i);
            }
        }
    }

    float GetTestRunCycle()
    {
        if(!Application.isPlaying)
            return editorRunTestsEvery;
        return runtimeRunTestsEvery;
    }

    [System.Serializable]
    public class Failure
    {
        // Has to be single class, to get it visible in inspector.
        [SerializeField] MonoBehaviour mono;
        [SerializeField] string msg;
        [SerializeField] object obj;

        public Failure(MonoBehaviour source, string v)
        {
            this.mono = source;
            this.msg = v;
        }

        public Failure(object source, string v)
        {
            this.obj = source;
            this.msg = v;
        }

        public string Message { get => msg; }
        public object AdditionalObject { get => mono != null ? mono : obj; }
        public MonoBehaviour Mono { get => mono; }
    }

    [Serializable]
    public class SceneSearchByClassType
    {
        [SerializeField] public BoolVarValue use;
        internal MonoBehaviour[] sceneCache;
        internal int[] foundTypes;
        internal List<ITestable> testableCache = new List<ITestable>();
        private List<ISetupUnity> setupsCache = new List<ISetupUnity>();

        public float editorSearchSceneEvery = 30;
        public float runtimeSearchSceneEvery = 30;

        Coroutine coro;

        Type[] defTypes =
        {
            typeof(ITestable),
            typeof(ISetupUnity)
        };

        public Coroutine RunSearch(RealtimeTester tester)
        {
            if (testableCache == null) testableCache = new List<ITestable>();
            if (coro == null)
                coro = tester.StartCoroutine(FindNewTargets(tester, defTypes));
            return coro;
        }


        private IEnumerator FindNewTargets(RealtimeTester tester, params Type[] types)
        {
            while (true)
            {
                if (!use.Value)
                {
                    yield return new WaitForSeconds(1);
                    continue;
                }

                sceneCache = GameObject.FindObjectsOfType<MonoBehaviour>();
                foundTypes = new int[sceneCache.Length];
                testableCache.Clear();
                setupsCache.Clear();
                NewCycleRealtimeTester(tester);
                for (int i = 0; i < sceneCache.Length; i++)
                {
                    foundTypes[i] = TypesContainScriptAt(types, i);

                    // log
                    if (foundTypes[i] == 0)
                    {
                        testableCache.Add((ITestable)sceneCache[i]);
                    }
                }
                yield return new WaitForSeconds(GetSearchCycle());
            }
        }
        private void NewCycleRealtimeTester(RealtimeTester tester)
        {
            tester.ResetFailedTests();
        }

        int TypesContainScriptAt(Type[] types, int sceneScriptId)
        {
            for (int j = 0; j < types.Length; j++)
            {
                if (types[j].IsAssignableFrom(sceneCache[sceneScriptId].GetType()))
                    return j;
            }
            return -1;
        }
        float GetSearchCycle()
        {
            if (!Application.isPlaying)
                return editorSearchSceneEvery;
            return runtimeSearchSceneEvery;
        }
    }

}
