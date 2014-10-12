#include <iostream>
#include <string>

using namespace std;

#pragma region в╟йндёй╫

class Person
{
public:
    Person(){}
    Person(string name) :name(name){}
    virtual ~Person(){}

public:
    virtual void Show()
    {
        cout << name << endl;
    }

private:
    string name;
};

class Finery : public Person
{
public:
    Finery(Person* p) :person(p){}
    virtual ~Finery(){}

public:
    virtual void Show()
    {
        person->Show();
    }

private:
    Person* person;
};

class Shirt : public Finery
{
public:
    Shirt(Person* p) :Finery(p){}
    virtual ~Shirt(){}

public:
    virtual void Show() override
    {
        cout << "Shirt ";
        Finery::Show();
    }
};

class Tie : public Finery
{
public:
    Tie(Person* p) :Finery(p){}
    virtual ~Tie(){}

public:
    virtual void Show() override
    {
        cout << "Tie ";
        Finery::Show();
    }
};

#pragma endregion

static inline void get_cpuid(unsigned int i, unsigned int *buf)
{
    unsigned int _eax,
        _ebx,
        _ecx,
        _edx;
    _asm
    {
        mov eax, i;
        cpuid;
        mov _eax, eax;
        mov _ebx, ebx;
        mov _ecx, ecx;
        mov _edx, edx;
    }

    buf[0] = _eax;
    buf[1] = _ebx;
    buf[2] = _ecx;
    buf[3] = _edx;
}

int main()
{
    unsigned int buffer[4];
    get_cpuid(1, buffer);

    printf("%x%x%x%x", buffer[0], buffer[1], buffer[2], buffer[3]);

    return 0;
}