// Program02.cpp: ����Ŀ�ļ���

#include "stdafx.h"

using namespace System;

ref class B
{
public:
    String^ Name;
};

Object^ Set(Object^ x, Object^ v)
{
    ((B^) x)->Name = (String^) v;
    return x;
}

int main(array<System::String ^> ^args)
{
    return 0;
}
