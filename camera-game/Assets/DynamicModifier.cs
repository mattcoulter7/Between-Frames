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
    NULL,
    FLOAT,
    STRING,
    INTEGER,
    BOOLEAN,
    OBJECT,
    CUSTOM_GETTER
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
            default:
                return null;
        }
    }
}

[System.Serializable]
public class ParamSet
{
    public ConfigurableParam[] parameters;
    public object[] GetParams()
    {
        return parameters.Select(p => p.GetParameter()).ToArray();
    }
}

public class TargetInfo
{
    public object parent;
    public string target;
    public object[] parameters;
    public System.Type parentType;
    
    private PropertyInfo _propertyInfo;
    private FieldInfo _fieldInfo;
    private MethodInfo _methodInfo;
    
    public TargetInfo(object parent,string target,object[] parameters)
    {
        this.target = target;
        this.parent = parent;
        this.parameters = parameters;
        parentType = parent.GetType();

        _propertyInfo = parentType.GetProperty(target);
        _fieldInfo = parentType.GetField(target);

        MethodInfo[] methods = parentType.GetMethods();
        _methodInfo = parentType.GetMethods().First(m => m.Name == target && m.GetParameters().Length == parameters.Length);
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
        if (_propertyInfo != null)
            return _propertyInfo.GetValue(parent, null);
        if (_fieldInfo != null)
            return _fieldInfo.GetValue(parent);
        if (_methodInfo != null)
            return _methodInfo.Invoke(parent, parameters);
        throw new InvalidTargetException(target, parentType);
    }

    public void SetTargetValue(object value)
    {
        if (_propertyInfo != null)
            _propertyInfo.SetValue(parent, value);
        else if (_fieldInfo != null)
            _fieldInfo.SetValue(parent, value);
        else if (_methodInfo != null)
        {
            if (parameters.Length > 0) parameters[parameters.Length - 1] = value;
            _methodInfo.Invoke(parent, parameters);
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

        // create TargetInfo objects for each chain link which allow us to easily get/set/call the property/method etc.
        for (int i = 0; i < targets.Length; i++)
        {
            Target target = targets[i];

            // store the target info reference
            TargetInfo targetInfo = new TargetInfo(parent, target.name, target.paramSet.GetParams());
            _chainTargets.Add(targetInfo);
        }
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
