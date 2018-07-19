using System;
using System.Drawing;
using System.Globalization;
using Foundation;
using Xamarin.Forms.Platform.iOS;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using ObjCRuntime;
using CoreFoundation;
using EventKit;
using System.Runtime.Serialization;
using System.Linq;

namespace Forms9Patch.iOS
{
    public static class NSObjectExtensions
    {
        public static NSDictionary ToNSDictionary(this IDictionary dictionary)
        {
            var dictionaryType = dictionary.GetType();
            if (!dictionaryType.IsGenericType)
                throw new Exception("Only works with Generic IDictionary objects");
            var genericArgs = dictionaryType.GetGenericArguments();
            if (genericArgs[0] != typeof(string))
                throw new Exception("Only works with Dictionary<string,T> objects");
            var nsDictionary = new NSMutableDictionary();
            foreach (var key in dictionary.Keys)
            {
                var nsItem = NSObject.FromObject(dictionary[key]);
                nsDictionary.Add(new NSString(key.ToString()), nsItem);
            }
            return nsDictionary;
        }


        public static NSArray ToNSArray(this IList list)
        {
            var nsArray = new NSMutableArray();
            var arrayType = list.GetType();
            bool isArrayOfDictionaries = arrayType.IsGenericType && arrayType.GetGenericArguments()[0].GetTypeInfo().ImplementedInterfaces.Contains(typeof(IDictionary));
            foreach (var item in list)
            {
                NSObject nsItem = null;
                if (isArrayOfDictionaries)
                    nsItem = ((IDictionary)item).ToNSDictionary();
                else
                    nsItem = NSObject.FromObject(item);
                nsArray.Add(nsItem);
            }
            return nsArray;
        }

        public static Tuple<object, Type> ToObject(this NSObject nsO)
        {
            return nsO.ToObject(null);
        }

        //      public enum TypeCode
        //      {
        //          Empty,
        //          Object,
        //          DBNull,
        //          Boolean,
        //          Char,
        //          SByte,
        //          Byte,
        //          Int16,
        //          UInt16,
        //          Int32,
        //          UInt32,
        //          Int64,
        //          UInt64,
        //          Single,
        //          Double,
        //          Decimal,
        //          DateTime,
        //          String = 18
        //      }

        public static Tuple<object, Type> ToDictionary(this NSDictionary nsDictionary)
        {
            var keyList = new List<string>();
            var valueList = new List<object>();
            var typeList = new List<Type>();

            foreach (var key in nsDictionary.Keys)
            {
                keyList.Add(key.ToString());
                try
                {
                    var nsObj = nsDictionary[key];
                    var tuple = nsObj.ToObject();
                    valueList.Add(tuple.Item1);
                    typeList.Add(tuple.Item2);
                }
                catch (Exception)
                {
                    valueList.Add(null);
                    typeList.Add(null);
                }
            }
            bool allSame = true;
            if (typeList.Count > 1)
            {
                for (int i = 1; i < typeList.Count; i++)
                    if (typeList[i] != typeList[0])
                    {
                        allSame = false;
                        break;
                    }
            }
            if (typeList.Count > 0)
            {
                var dictionaryType = typeof(Dictionary<,>);
                var elementType = allSame && typeList[0] != null ? typeList[0] : typeof(object);
                var constructedDictionaryType = dictionaryType.MakeGenericType(typeof(string), elementType);
                var result = (IDictionary)Activator.CreateInstance(constructedDictionaryType);
                //var result = Convert.ChangeType(itemList, constructedListType);  // List is not IConvertable
                for (int i = 0; i < keyList.Count; i++)
                {
                    result.Add(keyList[i].ToString(), valueList[i]);
                }
                return new Tuple<object, Type>(result, constructedDictionaryType);
            }
            return new Tuple<object, Type>(null, typeof(Dictionary<string, object>));
        }

        public static Tuple<object, Type> ToList(this NSArray nsArray)
        {
            //bool typeCodeSet = false;
            var itemList = new List<object>();
            var typeList = new List<Type>();
            for (nuint i = 0; i < nsArray.Count; i++)
            {
                try
                {
                    var nsObj = nsArray.GetItem<NSObject>(i);
                    var tuple = nsObj.ToObject();
                    itemList.Add(tuple.Item1);
                    typeList.Add(tuple.Item2);
                }
                catch (Exception)
                {
                    itemList.Add(null);
                    typeList.Add(null);
                }
            }

            bool allSame = true;
            if (typeList.Count > 1)
            {
                for (int i = 1; i < typeList.Count; i++)
                    if (typeList[i] != typeList[0])
                    {
                        allSame = false;
                        break;
                    }
            }
            if (allSame && typeList.Count > 0 && typeList[0] != null)
            {
                var listType = typeof(List<>);
                var elementType = typeList[0];
                var constructedListType = listType.MakeGenericType(elementType);
                var result = (IList)Activator.CreateInstance(constructedListType);
                //var result = Convert.ChangeType(itemList, constructedListType);  // List is not IConvertable
                foreach (var item in itemList)
                    result.Add(item);
                return new Tuple<object, Type>(result, constructedListType);
            }
            return new Tuple<object, Type>(itemList, typeof(List<object>));
        }

        public static bool Implements(this Type type, Type requestedInterface)
        {
            var typeInfo = type.GetTypeInfo();
            foreach (var implementedInterface in typeInfo.ImplementedInterfaces)
                if (implementedInterface == requestedInterface)
                    return true;
            return false;
        }

        public static Tuple<object, Type> ToObject(this NSObject nsO, Type type)
        {
            if (nsO is NSDictionary nsDictionary)
            {
                if (type == null)
                {
                    var tuple = nsDictionary.ToDictionary();
                    return tuple;
                }
                if (type.Implements(typeof(IDictionary)))
                {
                    IDictionary result;
                    Type genericType;
                    if (type.IsGenericType)
                    {
                        result = Activator.CreateInstance(type) as IDictionary;
                        genericType = type.GetTypeInfo().GenericTypeArguments[1];
                    }
                    else
                    {
                        result = new Dictionary<string, object>();
                        genericType = typeof(object);
                    }
                    foreach (var key in nsDictionary.Keys)
                    {
                        try
                        {
                            var nsObj = nsDictionary[key];
                            var tuple = nsObj.ToObject();
                            result.Add(key.ToString(), tuple.Item1);
                        }
                        catch (Exception)
                        {
                            result.Add(key.ToString(), null);
                        }
                    }
                    return new Tuple<object, Type>(result, type);
                }
                throw new InvalidDataContractException("Cannot reliablity convert NSDictionary to type [" + type + "].");
            }

            if (nsO is NSArray nsArray)
            {
                if (type == null)
                {
                    var tuple = nsArray.ToList();
                    return tuple;
                }

                if (type.Implements(typeof(IList)))
                {
                    IList result;
                    Type genericType;
                    if (type.IsGenericType)
                    {
                        result = Activator.CreateInstance(type) as IList;
                        genericType = type.GetTypeInfo().GenericTypeArguments[0];
                    }
                    else
                    {
                        result = new List<object>();
                        genericType = null;
                    }
                    //MethodInfo method = typeof(NSArray).GetMethod("FromArray");
                    //MethodInfo genericMethod = method.MakeGenericMethod(new[] { genericType });
                    //result = (IList)genericMethod.Invoke(null, new object[] { nsArray });
                    for (nuint i = 0; i < nsArray.Count; i++)
                    {
                        try
                        {
                            var nsObj = nsArray.GetItem<NSObject>(i);
                            var tuple = nsObj.ToObject(genericType);
                            result.Add(tuple.Item1);
                        }
                        catch (Exception)
                        {
                            result.Add(null);
                        }
                    }
                    return new Tuple<object, Type>(result, type);
                }
                throw new InvalidDataContractException("Cannot reliablity convert NSArray to type [" + type + "].");
            }

            if (nsO is NSString nsString)
                return new Tuple<object, Type>(nsString.ToString(), typeof(string));

            if (nsO is NSData nsData)
                return new Tuple<object, Type>(nsData.ToArray(), typeof(byte[]));

            if (nsO is NSDate nsDate)
                return new Tuple<object, Type>(DateTime.SpecifyKind(nsDate.ToDateTime(), DateTimeKind.Unspecified), typeof(DateTime));

            if (nsO is NSDecimalNumber nsDecimal)
                return new Tuple<object, Type>(decimal.Parse(nsDecimal.ToString(), CultureInfo.InvariantCulture), typeof(decimal));

            if (nsO is NSNumber nsNumber)
            {
                if (type == null)
                {
                    var objCType = nsNumber.ObjCType;
                    switch (objCType)
                    {
                        case "c": type = typeof(char); break;
                        case "i": type = typeof(int); break;
                        case "s": type = typeof(short); break;
                        case "l": type = typeof(int); break;
                        case "q": type = typeof(long); break;
                        case "C": type = typeof(byte); break;
                        case "S": type = typeof(ushort); break;
                        case "L": type = typeof(uint); break;
                        case "Q": type = typeof(ulong); break;

                        case "f": type = typeof(float); break;
                        case "d": type = typeof(double); break;
                        case "B": type = typeof(bool); break;
                        default:
                            // could be string(*), void (v), object(@), class object (#), array, structure, union, pointer,  or unknown
                            return null;
                    }
                }

                if (type == typeof(char))
                    return new Tuple<object, Type>((char)nsNumber.ByteValue, type);
                if (type == typeof(int))
                    return new Tuple<object, Type>((int)nsNumber.Int32Value, type);
                if (type == typeof(short))
                    return new Tuple<object, Type>((short)nsNumber.Int16Value, type);
                if (type == typeof(long))
                    return new Tuple<object, Type>((long)nsNumber.Int64Value, type);
                if (type == typeof(byte))
                    return new Tuple<object, Type>((byte)nsNumber.ByteValue, type);
                if (type == typeof(ushort))
                    return new Tuple<object, Type>((ushort)nsNumber.UInt16Value, type);
                if (type == typeof(uint))
                    return new Tuple<object, Type>((uint)nsNumber.UInt32Value, type);
                if (type == typeof(ulong))
                    return new Tuple<object, Type>((ulong)nsNumber.UInt64Value, type);
                if (type == typeof(float))
                    return new Tuple<object, Type>((float)nsNumber.FloatValue, type);
                if (type == typeof(double))
                    return new Tuple<object, Type>((double)nsNumber.DoubleValue, type);
                if (type == typeof(bool))
                    return new Tuple<object, Type>((bool)nsNumber.BoolValue, type);

            }

            if (nsO is NSUrl nsUrl)
            {
                var absolutePath = nsUrl.AbsoluteString;
                var uri = new Uri(absolutePath);
                return new Tuple<object, Type>(uri, typeof(Uri));
            }

            return null;
        }

    }
}