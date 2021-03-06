﻿#pragma once
#ifndef __BIBIM_UIWINDOW_H__
#define __BIBIM_UIWINDOW_H__

#include <Bibim/FWD.h>
#include <Bibim/UIPanel.h>

namespace Bibim
{
    class UIWindow : public UIPanel
    {
        BBComponentClass(UIWindow, UIPanel, 'U', 'W', 'N', 'D');
        public:
            UIWindow();
            explicit UIWindow(int childrenCapacity);
            virtual ~UIWindow();

            inline void AddChild(UIVisual* item);
            inline bool RemoveChild(UIVisual* item);
            inline bool RemoveChildAt(int index);
            inline void RemoveAllChildren();

            virtual bool IsWindow() const;

            static bool RemoveFromParent(UIVisual* item);
    };

    ////////////////////////////////////////////////////////////////////////////////////////////////////

    void UIWindow::AddChild(UIVisual* item)
    {
        Add(item);
    }

    bool UIWindow::RemoveChild(UIVisual* item)
    {
        return Remove(item);
    }

    bool UIWindow::RemoveChildAt(int index)
    {
        return RemoveAt(index);
    }

    void UIWindow::RemoveAllChildren()
    {
        RemoveAll();
    }
}

BBBindLua(Bibim::UIWindow);

#endif