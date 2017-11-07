﻿using System;
using System.Collections;
using System.Reflection;

namespace TypingsCreator.Core.TypeConversion
{
    public class TypeScriptTypeHandler
    {
        public string GetTypeScriptType(Type type)
        {
            switch (type.Name)
            {
                case "String":
                    return "string";
                case "Int":
                case "Int32":
                case "Int64":
                    return "number";
                case "Double":
                case "Single":
                    return "decimal";
                case "Boolean":
                    return "boolean";
                case "Void":
                    return "void";
                default:
                    if (IsCollection(type))
                    {
                        var collectionType = GetCollectionType(type);
                        return $"{collectionType}[]";
                    }
                    if (IsModel(type))
                    {
                        return GetTypeName(type);
                    }
                    return "any";
            }
        }

        public bool IsUnknownType(Type type)
        {
            return IsCollection(type) || (IsModel(type) && GetTypeScriptType(type) == GetTypeName(type));
        }

        public bool IsCollection(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return type.IsArray || typeInfo.IsGenericType && typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(typeInfo);
        }

        public Type GetTypeFromCollection(Type type)
        {
            if (IsCollection(type))
            {
                if (type.IsArray)
                {
                    var arrayType = type.GetElementType();
                    return arrayType;
                }
                var typeInfo = type.GetTypeInfo();
                if (typeInfo.IsGenericType)
                {
                    var genericType = typeInfo.GenericTypeArguments[0];
                    return genericType;
                }
            }
            return null;
        }

        private string GetCollectionType(Type type)
        {
            var collectionType = GetTypeFromCollection(type);
            if (collectionType != null)
            {
                return GetTypeScriptType(collectionType);
            }
            return null;
        }

        private string GetTypeName(Type type)
        {
            return type.Name;
        }

        private bool IsModel(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return !typeInfo.IsValueType && !(typeInfo.IsGenericType && !IsCollection(type));
        }
    }
}