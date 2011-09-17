#pragma once
#ifndef __BIBIM_AUTOLOCKER_H__
#define __BIBIM_AUTOLOCKER_H__

#   include <Bibim/FWD.h>
#   include <Bibim/Lock.h>

    namespace Bibim
    {
        class AutoLocker
        {
            BBThisIsNoncopyableClass(AutoLocker);
            public:
                inline AutoLocker(Lock& lock);
                inline ~AutoLocker();
            
            private:
                Lock& lock;
        };
    }

#   include <Bibim/AutoLocker.inl>

#endif