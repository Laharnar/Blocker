using UnityEngine;

public class TypeFactory
{
    IInstance[] templates;

    public TypeFactory(IInstance[] templates)
    {
        this.templates = templates;
    }

    public IInstance GetByType(int contentType)
    {
        for (int i = 0; i < templates.Length; i++)
        {
            int type = ReflectionGetter.GetStaticType(templates[i].GetType());
            if (type == contentType)
            {
                return templates[i].Instance();
            }
        }
        Debug.Log("Unhandled type " + contentType);
        return null;
    }
}
