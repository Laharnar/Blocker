﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestTheCamera
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestTheCameraSimplePasses()
        {
            // Use the Assert class to test conditions
            AssertIsCameraIdleOnStart();
        }


        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestTheCameraWithEnumeratorPasses()
        {

            yield return null;
        }

        void AssertIsCameraIdleOnStart()
        {

        }
    }
}