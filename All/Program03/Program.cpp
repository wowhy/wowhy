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

int main()
{
    Person *p = new Person("Jack");
    Shirt *s = new Shirt(p);
    Tie *t = new Tie(s);

    t->Show();
    s->Show();

    delete t;
    delete s;
    delete p;

    return 0;
}