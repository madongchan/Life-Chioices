using System;
using System.Collections.Generic;
using System.Linq;

public static class TextParser {
    /// <summary>
    /// 문자열을 ','를 구분자로 사용하여 나눈 후 각 값을 정수로 변환하여 리스트로 반환하는 메서드입니다.
    /// </summary>
    /// <param name="input">변환할 입력 문자열입니다.</param>
    /// <returns>정수 리스트입니다.</returns>
    public static List<int> ToIntList(this string input) {
        List<int> result = input
            .Split(',')
            .Select(str => int.Parse(str.Trim()))
            .ToList();

        return result;
    }
}
