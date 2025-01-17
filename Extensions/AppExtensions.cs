﻿using TerminalGPT.Constants;
using TerminalGPT.Options;

namespace TerminalGPT.Extensions;

public static class AppExtensions
{
    public static bool In<T>(this T obj, params T[] values)
    {
        return values.Contains(obj);
    }
    
    public static bool In(this object obj, params object[] values)
    {
        return values.Contains(obj);
    }

    public static string GetId(this GPTModel? model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        
        return model switch
        {
            GPTModel.GPT4 => AppConstants.ModelDictionary[GPTModel.GPT4],
            GPTModel.GPT4_32k => AppConstants.ModelDictionary[GPTModel.GPT4_32k],
            _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
        };
    }
    
    // generic extension method to get description from enum
    public static string GetDescription<T>(this T enumerationValue)
        where T : struct
    {
        var type = enumerationValue.GetType();
        if (!type.IsEnum)
        {
            throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type", nameof(enumerationValue));
        }

        var memberInfo = type.GetMember(enumerationValue.ToString());
        if (memberInfo.Length > 0)
        {
            var attrs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

            if (attrs.Length > 0)
            {
                return ((System.ComponentModel.DescriptionAttribute)attrs[0]).Description;
            }
        }

        return enumerationValue.ToString();
    }
    
    public static bool IsNullOrWhiteSpace(this string? str) => string.IsNullOrWhiteSpace(str);
}