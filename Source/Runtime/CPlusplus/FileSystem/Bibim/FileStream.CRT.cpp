#include <Bibim/PCH.h>
#include <Bibim/FileStream.CRT.h>
#include <Bibim/Assert.h>
#include <Bibim/Environment.h>
#include <Bibim/Math.h>
#include <Bibim/Numerics.h>
#include <Bibim/Log.h>

namespace Bibim
{
    static bool IsAbsolutePath(const String& path)
    {
        if (path.GetLength() >= 2)
        {
            const char* s = path.CStr();
            return (s[0] == '/' ||
                    s[1] == ':' ||
                    s[2] == ':');
        }

        return false;
    }

    FileStream::FileStream(const String& path, AccessMode accessMode)
        : handle(nullptr),
          canRead(false),
          canWrite(false)
    {
        if (path.IsEmpty())
            return;

        char mode[3] = { 'r', 'b', '\0' };
        if (accessMode == ReadOnly)
            mode[0] = 'r';
        else if (accessMode == WriteOnly)
            mode[0] = 'w';

        String cleanPath = path;
        cleanPath.Replace('\\', '/');

        if (IsAbsolutePath(cleanPath) == false)
        {
            BBAssert(cleanPath.CStr()[0] != '/');
            cleanPath = Environment::GetWorkingDirectory() + cleanPath;
        }

        handle = std::fopen(cleanPath.CStr(), mode);
        if (handle == nullptr)
        {
            canRead  = false;
            canWrite = false;
            return;
        }

        canRead  = (accessMode == FileStream::ReadOnly);
        canWrite = (accessMode == FileStream::WriteOnly);
    }

    FileStream::~FileStream()
    {
        Close();
    }

    void FileStream::Close()
    {
        if (handle)
        {
            Flush();
            std::fclose(handle);
            handle = nullptr;
        }
    }

    int FileStream::Read(void* buffer, int size)
    {
        if (handle == nullptr || size <= 0 || canRead == false)
            return 0;


        return static_cast<int>(std::fread(buffer, 1, size, handle));
    }

    int FileStream::Write(const void* buffer, int size)
    {
        if (handle == nullptr || size <= 0 || canWrite == false)
            return 0;

        return static_cast<int>(std::fwrite(buffer, 1, size, handle));
    }

    void FileStream::Flush()
    {
        if (handle == nullptr || canWrite == false)
            return;

        std::fflush(handle);
    }

    int FileStream::Seek(int offset, SeekOrigin origin)
    {
        int seekOrigin = 0;
        switch (origin)
        {
            case FromBegin:
                seekOrigin = SEEK_SET;
                break;
            case FromEnd:
                seekOrigin = SEEK_CUR;
                break;
            case FromCurrent:
                seekOrigin = SEEK_END;
                break;
        }

        const int current = static_cast<int>(std::ftell(handle));
        std::fseek(handle, offset, seekOrigin);
        return current;
    }

    int FileStream::GetPosition()
    {
        if (handle)
            return static_cast<int>(std::ftell(handle));
        else
            return 0;
    }

    int FileStream::GetLength()
    {
        if (handle == nullptr)
            return 0;

        const int current = static_cast<int>(std::ftell(handle));
        std::fseek(handle, 0, SEEK_END);
        const int length = std::ftell(handle);
        std::fseek(handle, current, SEEK_SET);

        return length;
    }

    bool FileStream::CanRead() const
    {
        return canRead;
    }

    bool FileStream::CanWrite() const
    {
        return canWrite;
    }

    bool FileStream::CanSeek() const
    {
        return true;
    }
}