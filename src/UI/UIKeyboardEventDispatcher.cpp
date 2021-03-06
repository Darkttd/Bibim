﻿#include <Bibim/Config.h>
#include <Bibim/UIKeyboardEventDispatcher.h>
#include <Bibim/Keyboard.h>
#include <Bibim/KeyboardState.h>
#include <Bibim/UIDomain.h>
#include <Bibim/UIKeyboardEventArgs.h>
#include <Bibim/UIWindow.h>

namespace Bibim
{
    UIKeyboardEventDispatcher::UIKeyboardEventDispatcher()
        : domain(nullptr),
          device(nullptr),
          lastState(KeyboardState::Empty)
    {
    }

    UIKeyboardEventDispatcher::UIKeyboardEventDispatcher(UIDomain* domain, Keyboard* device)
        : domain(domain),
          device(device),
          lastState(KeyboardState::Empty)
    {
    }

    UIKeyboardEventDispatcher::~UIKeyboardEventDispatcher()
    {
    }

    void UIKeyboardEventDispatcher::Update(float /*dt*/, int timestamp)
    {
        if (domain == nullptr ||
            device == nullptr ||
            GetStatus() != ActiveStatus ||
            Seal(timestamp))
            return;

        UIVisualPtr target = domain->GetFocus();
        if (target == nullptr)
            target = domain->GetRoot();
        if (target == nullptr)
            return;

        const KeyboardState& state = device->GetState();

        const Key::Code ctrl = state.IsPressed(Key::Ctrl) ? Key::Ctrl : Key::None;
        const Key::Code alt = state.IsPressed(Key::Alt) ? Key::Alt : Key::None;
        const Key::Code shift = state.IsPressed(Key::Shift) ? Key::Shift : Key::None;

        int code = 0;
        for (int i = 0; i < sizeof(state.Keys) / sizeof(state.Keys[0]); i++)
        {
            const int currentField = state.Keys[i];
            const int lastField    = lastState.Keys[i];
            for (unsigned int k = 0x80000000; k != 0x00000000; k >>= 1, code++)
            {
                const bool lastDown    = (lastField & k) != 0x00000000;
                const bool currentDown = (currentField & k) != 0x00000000;

                if (currentDown)
                {
                    if (lastDown == false)
                        target->RaiseKeyDownEvent(UIKeyboardEventArgs(this,
                                                                      target,
                                                                      static_cast<Key::Code>(code),
                                                                      ctrl, alt, shift));

                    target->RaiseKeyPressingEvent(UIKeyboardEventArgs(this, target, static_cast<Key::Code>(code)));
                }
                else
                {
                    if (lastDown)
                        target->RaiseKeyUpEvent(UIKeyboardEventArgs(this, target, static_cast<Key::Code>(code)));
                }
            }
        }

        lastState = state;
    }

    void UIKeyboardEventDispatcher::SetDomain(UIDomain* value)
    {
        if (domain != value)
        {
            domain = value;
            lastState = KeyboardState::Empty;
        }
    }

    void UIKeyboardEventDispatcher::SetDevice(Keyboard* value)
    {
        if (device != value)
        {
            device = value;
            lastState = KeyboardState::Empty;
        }
    }
}