using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System;

class InvalidTargetException : Exception
{
    public InvalidTargetException(string target,System.Type type) : base($"{target} does not exist as a Property, Field or Method on Type {type.Name}")
    {
    }
}


[System.Serializable]
public enum ParamType{ 
    NULL = 0,
    FLOAT = 1,
    STRING = 2,
    INTEGER = 3,
    BOOLEAN = 4,
    OBJECT = 5,
    CUSTOM_GETTER = 6,
    INITIALISE_PARAM = 7
}

[System.Serializable]
public class InitialiseParam {
    public string typeName;
    public ParamSet paramSet;
    public void OnInitialise()
    {
        paramSet.OnInitialise();
    }
    public object GetObject()
    {
        Type t = Type.GetType(typeName);
        
        return Activator.CreateInstance(t, paramSet.GetParams());
    }
}


[System.Serializable]
public class ConfigurableParam {
    public ParamType paramType;

    public float floatParameter;
    public string stringParameter;
    public int intParameter;
    public bool boolParameter;
    public DynamicModifier customGetter;
    public UnityEngine.Object objectParameter;
    public InitialiseParam initialiseParameter;
    public void OnInitialise()
    {
        switch (paramType)
        {
            case ParamType.CUSTOM_GETTER:
                customGetter.OnInitialise();
                break;
            case ParamType.INITIALISE_PARAM:
                initialiseParameter.OnInitialise();
                break;
            default:
                break;
        }
    }

    public object GetParameter()
    {
        switch (paramType) {
            case ParamType.NULL:
                return null;
            case ParamType.FLOAT:
                return floatParameter;
            case ParamType.BOOLEAN:
                return boolParameter;
            case ParamType.INTEGER:
                return intParameter;
            case ParamType.OBJECT:
                return objectParameter;
            case ParamType.STRING:
                return stringParameter;
            case ParamType.CUSTOM_GETTER:
                return customGetter.GetValue();
            case ParamType.INITIALISE_PARAM:
                return initialiseParameter.GetObject();
            default:
                return null;
        }
    }
}

[System.Serializable]
public class ParamSet
{
    public ConfigurableParam[] parameters;
    public void OnInitialise()
    {
        foreach (ConfigurableParam param in parameters)
        {
            param.OnInitialise();
        }
    }
    public object[] GetParams()
    {
        return parameters.Select(p => p.GetParameter()).ToArray();
    }
}

public class TargetInfo
{
    public object parent;
    public TargetInfo parentTargetInfo;
    public string target;
    public ParamSet paramSet;
    public System.Type parentType;

    private object GetParent() => parentTargetInfo == null ? parent : parentTargetInfo.GetTargetValue();
    private PropertyInfo _propertyInfo;
    private FieldInfo _fieldInfo;
    private MethodInfo _methodInfo;
    
    public TargetInfo(object parent, TargetInfo parentTargetInfo,string target, ParamSet paramSet)
    {
        this.target = target;
        this.parent = parent;
        this.parentTargetInfo = parentTargetInfo;
        this.paramSet = paramSet;
        parentType = GetParent().GetType();

        _propertyInfo = parentType.GetProperty(target);
        _fieldInfo = parentType.GetField(target);

        MethodInfo[] methods = parentType.GetMethods();
        try
        {
            _methodInfo = parentType.GetMethods().First(m => m.Name == target && m.GetParameters().Length == paramSet.parameters.Length);
        }
        catch (InvalidOperationException e) { }
    }

    public System.Type GetTargetType()
    {
        // DETERMINE THE NEW TYPE
        if (_propertyInfo != null)
            return _propertyInfo.PropertyType;
        if (_fieldInfo != null)
            return _fieldInfo.FieldType;
        if (_methodInfo != null)
            return _methodInfo.ReturnType;
        throw new InvalidTargetException(target, parentType);
    }
    public object GetTargetValue()
    {
        object p = GetParent();
        if (_propertyInfo != null)
            return _propertyInfo.GetValue(p, null);
        if (_fieldInfo != null)
            return _fieldInfo.GetValue(p);
        if (_methodInfo != null)
            return _methodInfo.Invoke(p, paramSet.GetParams());
        throw new InvalidTargetException(target, parentType);
    }

    public void SetTargetValue(object value)
    {
        object p = GetParent();
        if (_propertyInfo != null)
            _propertyInfo.SetValue(p, value);
        else if (_fieldInfo != null)
            _fieldInfo.SetValue(p, value);
        else if (_methodInfo != null)
        {
            object[] parameters = paramSet.GetParams();
            if (parameters.Length > 0) parameters[parameters.Length - 1] = value;
            _methodInfo.Invoke(p, parameters);
        }
        else 
            throw new InvalidTargetException(target, parentType);
    }

    public bool IsMethod()
    {
        return _methodInfo != null;
    }
}

[System.Serializable]
public struct Target
{
    public string name; // chain which which direct compiler to the property/method 
    public ParamSet paramSet; // method path which returns the current value
    public Target(string name, ParamSet paramSet)
    {
        this.name = name;
        this.paramSet = paramSet;
    }
}

[System.Serializable]
public class DynamicModifier
{
    public UnityEngine.Object objectReference; // component for where the chain attaches to
    public Target[] targets; // chain which which direct compiler to the property/method 

    private List<TargetInfo> _chainTargets;
    public void OnInitialise()
    {
        _chainTargets = new List<TargetInfo>();

        object parent = objectReference;
        TargetInfo parentTargetInfo = null;

        // create TargetInfo objects for each chain link which allow us to easily get/set/call the property/method etc.
        for (int i = 0; i < targets.Length; i++)
        {
            Target target = targets[i];

            // store the target info reference
            target.paramSet.OnInitialise();
            TargetInfo targetInfo = new TargetInfo(parent, parentTargetInfo, target.name, target.paramSet);
            parentTargetInfo = targetInfo;

            _chainTargets.Add(targetInfo);
        }
    }
    public object Invoke()
    {
        TargetInfo targetInfo = _chainTargets[_chainTargets.Count - 1];
        return targetInfo.GetTargetValue();
    }
    public object GetValue()
    {
        TargetInfo targetInfo = _chainTargets[_chainTargets.Count - 1];
        return targetInfo.GetTargetValue();
    }

    public void SetValue(object value)
    {
        TargetInfo targetInfo = _chainTargets[_chainTargets.Count - 1];
        targetInfo.SetTargetValue(value);
    }
}
