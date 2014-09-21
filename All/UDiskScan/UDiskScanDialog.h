#pragma once

#include <Windows.h>
#include <WindowsX.h>
#include <Dbt.h>

#include "resource.h"

class HandlerMessage
{
public:
    HWND hwnd;
    UINT message;
    WPARAM wParam;
    LPARAM lParam;
};

class Dialog
{
public:
    virtual bool OnInit()
    {
        return true;
    }

    virtual bool OnClose()
    {
        return true;
    }

    virtual void OnDispose()
    {
    }

public:
    int res;
};

class UDiskScanDialog : public Dialog
{
public:
    UDiskScanDialog()
    {
        this->res = IDD_UDISKDIALOG;
    }

    ~UDiskScanDialog(){}

public:

private:
};

class Application
{
public:
    static Dialog* CurrentDialog;

    static void Run(Dialog dialog)
    {
        Application::CurrentDialog = &dialog;
        DialogBox((HINSTANCE)GetModuleHandle(NULL), MAKEINTRESOURCE(dialog.res), NULL, DialogProc);
    }

    static INT_PTR CALLBACK DialogProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
    {
        switch (message)
        {
        case WM_INITDIALOG:
            return CurrentDialog->OnInit();

        case WM_CLOSE:
            if (CurrentDialog->OnClose())
            {
                CurrentDialog->OnDispose();
                EndDialog(hwnd, 0);
                PostQuitMessage(0);
            }

            break;
        }

        return FALSE;
    }
};

Dialog* Application::CurrentDialog = nullptr;