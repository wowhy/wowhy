// Program02.cpp: ����Ŀ�ļ���

#include "stdafx.h"

using namespace System;

value struct B
{
public:
    String^ Name;
};

void Set(Object^ &x, Object^ v)
{
    B b = (B)x;
    b.Name = (String^)v;
    x = b;
}

int main(array<System::String ^> ^args)
{
    return 0;
}
