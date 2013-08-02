# -*- coding:utf-8 -*-
import os

MODULES = (
    'Animation',
    'Asset',
    'Audio',
    'Collision2D',
    'Component',
    'Diagnostics',
    'FileSystem',
    'Foundation',
    'Framework',
    'Gameplay',
    'GFX',
    'Graphics',
    'Input',
    'Module',
    'Net',
    'Scripting',
    'Threading',
    'Time',
    'Typography',
    'UI',
)

# PRODUCT_DIR = '../../Product/'
# INCLUDE_DIR = os.path.join(PRODUCT_DIR, 'Include')
# LIBRARY_DIR = os.path.join(PRODUCT_DIR, 'Lib')

# PRODUCT_ABS_DIR = '$(BIBIM_DIR)Product/'
# INCLUDE_ABS_DIR = os.path.join(PRODUCT_ABS_DIR, 'Include')
# LIBRARY_ABS_DIR = os.path.join(PRODUCT_ABS_DIR, 'Lib')


def file_ext(filename):
    return os.path.splitext(filename)[1][1:]


# def copy_headers(module_name):
#     from shutil import copy2, copystat
#     src = './Cplusplus/{0}/Bibim/'.format(module_name)
#     dst = os.path.join(INCLUDE_DIR, 'Bibim')
#     names = os.listdir(src)

#     errors = []
#     for name in names:
#         if (file_ext(name) not in ('h',)):
#             continue

#         srcname = os.path.join(src, name)
#         dstname = os.path.join(dst, name)
#         try:
#             copy2(srcname, dstname)
#         except (IOError, os.error) as why:
#             errors.append((srcname, dstname, str(why)))
#         except Error as err:
#             errors.extend(err.args[0])
#     try:
#         copystat(src, dst)
#     except WindowsError:
#         pass
#     except OSError as why:
#         errors.extend((src, dst, str(why)))
#     if errors:
#         raise Error(errors)
        

# def copy_references_header(src):
#     from shutil import copy2
#     copy2(src, INCLUDE_DIR)


class PLATFORM:
    WIN32 = 1
    ANDROID = 2
    IOS = 3

    @staticmethod
    def to_string(code):
        if (code == PLATFORM.WIN32):
            return 'Win32'
        elif (code == PLATFORM.ANDROID):
            return 'Android'
        elif (code == PLATFORM.IOS):
            return 'iOS'
        else:
            return None

    
class TARGET:
    DEBUG = 1
    RELEASE = 2
    
    @staticmethod
    def to_string(code):
        if (code == TARGET.DEBUG):
            return 'Debug'
        elif (code == TARGET.RELEASE):
            return 'Release'
        else:
            return None
    
    
class ENVIRONMENT:
    VS2008 = 1
    VS2010 = 2

    @staticmethod
    def to_string(code):
        if (code == ENVIRONMENT.VS2008):
            return 'vs2008'
        elif (code == ENVIRONMENT.VS2010):
            return 'vs2010'
        else:
            return None
    
    
def get_include_directories(environment, platform, target):
    l = []
    l += ['$(BIBIM_DIR)\include']

    if (platform == PLATFORM.WIN32):
        l += ['$(DXSDK_DIR)Include']
    
    return l
    
    
def get_library_directories(environment, platform, target):
    l = []
    if (platform == PLATFORM.WIN32):
        l += ['$(DXSDK_DIR)Lib/x86']
    
    return l    

    
def get_dependencies(environment, platform, target):
    l = []
    win32 = platform == PLATFORM.WIN32
    android = platform == PLATFORM.ANDROID
    ios = platform == PLATFORM.IOS
    debug = target == TARGET.DEBUG
    release = target == TARGET.RELEASE
    vs2008 = environment == ENVIRONMENT.VS2008
    vs2010 = environment == ENVIRONMENT.VS2010

    # BIBIM
    if (win32):
        l += ['Bibim.lib']
    elif (android):
        l += ['Bibim.a']
    
    # Platform
    if (win32):
        l += ['winmm.lib']
        l += ['ws2_32.lib']
    
    # DirectX9
    if (win32):
        l += ['d3d9.lib']
        if (debug):
            l += ['d3dx9d.lib']
        elif (release):
            l += ['d3dx9.lib']
     
    # ZLib & PNG & JPG & FreeType & Lua & SPARK & Curl
    if (win32):
        if (debug):
            if (vs2008):
                l += ['extlibs/zlib-1.2.8/lib/Win32/Debug/vs2008/zlib.lib']
                l += ['extlibs/libpng-1.6.3/lib/Win32/Debug/vs2008/libpng.lib']
                l += ['extlibs/jpeg-9/lib/Win32/Debug/vs2008/libjpeg.lib']
                l += ['extlibs/freetype-2.5.0.1/custom/lib/Win32/Debug/vs2008/freetype.lib']
                l += ['extlibs/lua-5.2.2/lib/Win32/Debug/vs2008/lua.lib']
                l += ['extlibs/SPARK-1.5.5/lib/vc2008/static/SPARK_debug.lib']
                l += ['extlibs/curl-7.29.0/build/Win32/LIB Debug/vs2008/libcurl.lib']
            elif (vs2010):
                l += ['extlibs/zlib-1.2.8/lib/Win32/Debug/vs2010/zlib.lib']
                l += ['extlibs/libpng-1.6.3/lib/Win32/Debug/vs2010/libpng.lib']
                l += ['extlibs/jpeg-9/lib/Win32/Debug/vs2010/libjpeg.lib']
                l += ['extlibs/freetype-2.5.0.1/custom/lib/Win32/Debug/vs2010/freetype.lib']
                l += ['extlibs/lua-5.2.2/lib/Win32/Debug/vs2010/lua.lib']
                l += ['extlibs/SPARK-1.5.5/lib/vc2010/static/SPARK_debug.lib']
                l += ['extlibs/curl-7.29.0/build/Win32/LIB Debug/vs2010/libcurl.lib']
        elif (release):
            if (vs2008):
                l += ['extlibs/zlib-1.2.8/lib/Win32/Release/vs2008/zlib.lib']
                l += ['extlibs/libpng-1.6.3/lib/Win32/Release/vs2008/libpng.lib']
                l += ['extlibs/jpeg-9/lib/Win32/Release/vs2008/libjpeg.lib']
                l += ['extlibs/freetype-2.5.0.1/custom/lib/Win32/Release/vs2008/freetype.lib']
                l += ['extlibs/lua-5.2.2/lib/Win32/Release/vs2008/lua.lib']
                l += ['extlibs/SPARK-1.5.5/lib/vc2008/static/SPARK.lib']
                l += ['extlibs/curl-7.29.0/build/Win32/LIB Release/vs2008/libcurl.lib']
            elif (vs2010):
                l += ['extlibs/zlib-1.2.8/lib/Win32/Release/vs2010/zlib.lib']
                l += ['extlibs/libpng-1.6.3/lib/Win32/Release/vs2010/libpng.lib']
                l += ['extlibs/jpeg-9/lib/Win32/Release/vs2010/libjpeg.lib']
                l += ['extlibs/freetype-2.5.0.1/custom/lib/Win32/Release/vs2010/freetype.lib']
                l += ['extlibs/lua-5.2.2/lib/Win32/Release/vs2010/lua.lib']
                l += ['extlibs/SPARK-1.5.5/lib/vc2010/static/SPARK.lib']
                l += ['extlibs/curl-7.29.0/build/Win32/LIB Release/vs2010/libcurl.lib']
    elif (android):
        if (debug):
            l += ['extlibs/libpng-1.6.3/lib/Android/Debug/vs2010/libpng.a']
            l += ['extlibs/zlib-1.2.8/lib/Android/Debug/vs2010/zlib.a']
            l += ['extlibs/jpeg-9/lib/Android/Debug/vs2010/libjpeg.a']
            l += ['extlibs/freetype-2.5.0.1/custom/lib/Android/Debug/vs2010/freetype.a']
            l += ['extlibs/lua-5.2.2/lib/Android/Debug/vs2010/lua.a']
            l += ['extlibs/SPARK-1.5.5/lib/vc2010/static/SPARK_android_debug.a']
            l += ['extlibs/curl-7.29.0/build/Android/LIB Debug/vs2010/libcurl.a']
        elif (release):
            l += ['extlibs/libpng-1.6.3/lib/Android/Release/vs2010/libpng.a']
            l += ['extlibs/zlib-1.2.8/lib/Android/Release/vs2010/zlib.a']
            l += ['extlibs/jpeg-9/lib/Android/Release/vs2010/libjpeg.a']
            l += ['extlibs/freetype-2.5.0.1/custom/lib/Android/Release/vs2010/freetype.a']
            l += ['extlibs/lua-5.2.2/lib/Android/Release/vs2010/lua.a']
            l += ['extlibs/SPARK-1.5.5/lib/vc2010/static/SPARK_android.a']
            l += ['extlibs/curl-7.29.0/build/Android/LIB Release/vs2010/libcurl.a']
    
    # irrKlang
    if (win32):
        l += ['extlibs/irrKlang-1.4.0/lib/Win32-visualStudio/irrKlang.lib']
        
    # OpenAL
    if (win32):
        l += ['extlibs/OpenAL/PC/libs/Win32/OpenAL32.lib']
    elif (android):
        l += ['extlibs/OpenAL/Android/libs/armeabi/libopenal.so']
        l += ['extlibs/OpenAL/Android/libs/armeabi/libopenalwrapper.so']
        
    # MPQ
    if (win32):
        if (vs2008):
            if (debug):
                l += ['extlibs/StormLib/lib/Win32/Debug/vs2008/StormLibDAD.lib']
            elif (release):
                l += ['extlibs/StormLib/lib/Win32/Release/vs2008/StormLibRAD.lib']
        elif (vs2010):
            if (debug):
                l += ['extlibs/StormLib/lib/Win32/Debug/vs2010/StormLibDAD.lib']
            elif (release):
                l += ['extlibs/StormLib/lib/Win32/Release/vs2010/StormLibRAD.lib']

    return l
        

def save_xml(filename, element):
    import xml.etree.ElementTree as ET
    xml_text = ET.tostring(element, 'utf-8')
    
    f = open(filename, 'w')
    f.write('<?xml version="1.0" encoding="utf-8"?>\n')
    f.write(xml_text)
    f.close()
    
    
def get_main_library_directory(environment, platform, target):    
    environment = ENVIRONMENT.to_string(environment)
    platform = PLATFORM.to_string(platform)
    target = TARGET.to_string(target)
    
    components = ['lib']
    if (environment):
        components.append(environment)
    if (platform):
        components.append(platform)
    if (target):
        components.append(target)

    return os.path.join(*components)


def make_visual_studio_2008_property_sheet(name, 
                                           include_directories,
                                           library_directories,
                                           depdendencies):
                                     
    import xml.etree.ElementTree as ET
    root = ET.Element('VisualStudioPropertySheet', {
        'ProjectType': 'Visual C++',
        'Version': '8.00',
        'Name': name,
    })
    
    root.append(ET.Element('Tool', {
        'Name': 'VCCLCompilerTool',
        'AdditionalIncludeDirectories': include_directories.replace('/', '\\'),
        'PreprocessorDefinitions': 'CURL_STATICLIB',
        'WarningLevel': '4',
    }))
    root.append(ET.Element('Tool', {
        'Name': 'VCLinkerTool',
        'AdditionalDependencies': depdendencies.replace('/', '\\').replace(';', ' '),
        'AdditionalLibraryDirectories': library_directories.replace('/', '\\'),
    }))
    
    return root
    
    
def make_visual_studio_2010_property_sheet(name,
                                           include_directories,
                                           library_directories,
                                           depdendencies,
                                           platform=None):
    
    import xml.etree.ElementTree as ET
    root = ET.Element('Project', {
        'DefaultTargets': 'Build',
        'ToolsVersion': '4.0',
        'xmlns': 'http://schemas.microsoft.com/developer/msbuild/2003',
    })
    
    def ETElementWithText(element, text):
        element = ET.Element(element)
        element.text = text
        return element
    
    property_group = ET.Element('PropertyGroup')
    property_group.append(ETElementWithText(
        '_ProjectFileVersion_ProjectFileVersion',
        '10.0.40219.1'
    ))
    root.append(property_group)
    
    item_definition_group = ET.Element('ItemDefinitionGroup')
    cl_compile = ET.Element('ClCompile')
    cl_compile.append(ETElementWithText(
        'AdditionalIncludeDirectories',
        include_directories.replace('/', '\\') + ';%(AdditionalIncludeDirectories)'
    ))
    cl_compile.append(ETElementWithText('WarningLevel', 'Level4'))
    item_definition_group.append(cl_compile)
    link = ET.Element('Link')
    link.append(ETElementWithText(
        'AdditionalDependencies',
        depdendencies.replace('/', '\\') + ';%(AdditionalDependencies)'
    ))
    link.append(ETElementWithText(
        'AdditionalLibraryDirectories',
        library_directories.replace('/', '\\') + ';%(AdditionalLibraryDirectories)'
    ))
    if (platform and platform == PLATFORM.ANDROID):
        link.append(ETElementWithText(
            'AdditionalOptions',
            '-lGLESv2 -landroid %(AdditionalOptions)'
        ))
    item_definition_group.append(link)
    root.append(item_definition_group)
    
    return root
    
    
def publish_library(environment, platform, target):
    name = 'Bibim'
    main_library_rel_directory = get_main_library_directory(environment, platform, target)
    main_library_abs_directory = '$(BIBIM_DIR){0}'.format(main_library_rel_directory)
    
    # �߰� ���� ���͸��� ���̺귯�� ���͸��� ���մϴ�.
    include_directories = get_include_directories(environment, platform, target)
    include_directory = ';'.join(map(lambda o: '"{0}"'.format(o), include_directories))
    library_directories = [main_library_abs_directory]
    library_directories += get_library_directories(environment, platform, target)
    library_directory = ';'.join(map(lambda o: '"{0}"'.format(o), library_directories))
        
    # ���� ���̺귯���� ����� ���մϴ�.
    dependencies = get_dependencies(environment, platform, target)
    if (platform == PLATFORM.ANDROID):
        dependency_filenames = ';'.join(map(
            lambda o: os.path.join(main_library_abs_directory, os.path.basename(o)),
            dependencies
        ))
    else:
        dependency_filenames = ';'.join(map(
            lambda o: os.path.basename(o),
            dependencies
        ))
    
    # ȯ�濡 �´� ������Ƽ ��Ʈ�� �����ؼ� �����մϴ�.
    if (environment == ENVIRONMENT.VS2008):
        element = make_visual_studio_2008_property_sheet(name,
                                                         include_directory,
                                                         library_directory,
                                                         dependency_filenames)
        save_xml('{0}.vsprops'.format(os.path.join(main_library_rel_directory, name)), element)
    elif (environment == ENVIRONMENT.VS2010):
        element = make_visual_studio_2010_property_sheet(name,
                                                         include_directory,
                                                         library_directory,
                                                         dependency_filenames,
                                                         platform)
        save_xml('{0}.props'.format(os.path.join(main_library_rel_directory, name)), element)
 
    # ���� ���̺귯������ Product ������ �����մϴ�.
    # library_directory = os.path.join(LIBRARY_DIR, library_relative_directory)
    # from shutil import copy2
    # for item in dependencies:
    #     if (item.startswith('extlibs')):  # ���ÿ� �ִ� ���̺귯���� �����մϴ�.
    #         copy2(item, library_directory)

            
if (__name__ == '__main__'):
    # import sys
    # header_only = (len(sys.argv) >= 2 and sys.argv[1] == 'header-only')
    
    # print('publish headers...')

    # for module in MODULES:
    #     copy_headers(module)
    # copy_references_header('extlibs/lua-5.2.2/src/lua.h')
    # copy_references_header('extlibs/lua-5.2.2/src/lauxlib.h')
    # copy_references_header('extlibs/lua-5.2.2/src/luaconf.h')
    # copy_references_header('extlibs/lua-5.2.2/src/lualib.h')
    # copy_references_header('extlibs/lua_tinker/lua_tinker.h')
    
    # copy_references_header('ObjectiveC/Bibim/Bibim/BibimAppDelegate.h')
    # copy_references_header('ObjectiveC/Bibim/Bibim/BibimViewController.h')
    
    # if (not header_only):
    print('publish libraries...')
        
    publish_library(ENVIRONMENT.VS2008, PLATFORM.WIN32, TARGET.DEBUG)
    publish_library(ENVIRONMENT.VS2008, PLATFORM.WIN32, TARGET.RELEASE)

    publish_library(ENVIRONMENT.VS2010, PLATFORM.WIN32,   TARGET.DEBUG)
    publish_library(ENVIRONMENT.VS2010, PLATFORM.WIN32,   TARGET.RELEASE)
    publish_library(ENVIRONMENT.VS2010, PLATFORM.ANDROID, TARGET.DEBUG)
    publish_library(ENVIRONMENT.VS2010, PLATFORM.ANDROID, TARGET.RELEASE)