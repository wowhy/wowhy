// Program02.cpp: 主项目文件。

#include <iostream>
#include <string>

using namespace System;
using namespace System::Threading;
using namespace System::Reflection;
using namespace System::Runtime::Remoting;

#define OUTPUT_APPDOMAIN() Console::WriteLine("Executing in " + Thread::GetDomain()->FriendlyName);

[Serializable]
public ref class MarshalByValType : public Object
{
public:
    MarshalByValType() :m_creationTime(DateTime::Now)
    {
        Console::WriteLine(
            "{0} ctor running in {1}, Created on {2:D}",
            this->GetType()->ToString(),
            Thread::GetDomain()->FriendlyName,
            m_creationTime);
    }

    String^ ToString() override
    {
        return m_creationTime.ToLongDateString();
    }

private:
    DateTime m_creationTime;
};

public ref class MarshalByRefType : public MarshalByRefObject
{
public:
    MarshalByRefType()
    {
        Console::WriteLine(
            "{0} ctor running in {1}",
            this->GetType()->ToString(),
            Thread::GetDomain()->FriendlyName);
    }

    void SomeMethod()
    {
        OUTPUT_APPDOMAIN();
    }

    MarshalByValType^ MethodWithReturn()
    {
        OUTPUT_APPDOMAIN();

        return gcnew MarshalByValType();
    }
};

public ref class NonMarshalableType :public Object
{
public:
    NonMarshalableType()
    {
        OUTPUT_APPDOMAIN();
    }
};

void Marshalling()
{
    auto adCallingThreadDomain = Thread::GetDomain();

    auto callingDomainName = adCallingThreadDomain->FriendlyName;
    Console::WriteLine("Default AppDomain's friendly name={0}", callingDomainName);

    auto exeAssembly = Assembly::GetEntryAssembly()->FullName;
    Console::WriteLine("Main assembly={0}", exeAssembly);

    Console::WriteLine();
    Console::WriteLine();

    Console::WriteLine("Demo #1");
    auto ad2 = AppDomain::CreateDomain("AD #2", nullptr, nullptr);
    auto mbrt = dynamic_cast<MarshalByRefType^>(ad2->CreateInstanceAndUnwrap(exeAssembly, "MarshalByRefType"));

    Console::WriteLine("Type={0}", mbrt->GetType());
    Console::WriteLine("Is Proxy={0}", RemotingServices::IsTransparentProxy(mbrt));

    mbrt->SomeMethod();

    AppDomain::Unload(ad2);

    try
    {
        mbrt->SomeMethod();
    }
    catch (AppDomainUnloadedException^)
    {
        Console::WriteLine("Failed call.");
    }

    Console::WriteLine();
    Console::WriteLine("Demo #2");

    ad2 = AppDomain::CreateDomain("AD #2", nullptr, nullptr);
    mbrt = dynamic_cast<MarshalByRefType^>(ad2->CreateInstanceAndUnwrap(exeAssembly, "MarshalByRefType"));

    auto mbvt = mbrt->MethodWithReturn();

    Console::WriteLine("Is Proxy={0}", RemotingServices::IsTransparentProxy(mbvt));
    Console::WriteLine("Returned object created " + mbvt->ToString());

    AppDomain::Unload(ad2);

    try
    {
        Console::WriteLine("Returned object created " + mbvt->ToString());
        Console::WriteLine("Successful call.");
    }
    catch (AppDomainUnloadedException^)
    {
        Console::WriteLine("Failed call.");
    }
}

class NativeClass
{
public:
    NativeClass():name("")
    {
    }

    std::string getName() const { return this->name; }
    void setName(std::string name)
    {
        this->name = name;
    }

private:
    std::string name;
};

int main(array<System::String ^> ^args)
{
    Marshalling();

    auto p = new NativeClass();
    p->setName("test");

    std::cout << p->getName() << std::endl;

    delete p;
    return 0;
}