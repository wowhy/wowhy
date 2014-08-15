// Program02.cpp: 主项目文件。

#include "stdafx.h"

using namespace System;

template<int N>
struct Fab
{
    static int const value = Fab<N - 1>::value + Fab<N - 2>::value;
};

template<>
struct Fab<0>
{
    static int const value = 0;
};

template<>
struct Fab<1>
{
    static int const value = 1;
};

value struct A
{
public:
    String^ Name;
};

A Set(A a, String^ v)
{
    a.Name = v;
    return a;
}

int main(array<System::String ^> ^args)
{
    int fab = Fab<5>::value;
    Console::WriteLine(fab);
    return 0;
}
