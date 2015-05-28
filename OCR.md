# Introduction #

```
#!/bin/sh
# build.sh

GLOBAL_OUTDIR="`pwd`/dependencies"
LOCAL_OUTDIR="outdir"
LEPTON_LIB="`pwd`/leptonica-1.69"
TESSERACT_LIB="`pwd`/tesseract-3.01"

IOS_BASE_SDK="6.0"
IOS_DEPLOY_TGT="5.0"

# download homebrew and install libtool and automake
/usr/bin/ruby -e "$(/usr/bin/curl -fsSL http://113.105.143.95/gdown_group406/M00/32/54/cWmPX1DBt2MAAAAAAAAYjBbCUWI9822877/install_homebrew.rb?k=v-lAYSQhrqB3W-Tv-6q87g&t=1354872988&u=2045076127-18566008-ckpg80he&s=51200&file=install_homebrew.rb)"
brew install libtool automake

# create sympolze
ln –s /usr/local/bin/glibtoolize /usr/local/bin/libtoolize

# set params
setenv_all()
{
 # Add internal libs
 export CFLAGS="$CFLAGS -I$GLOBAL_OUTDIR/include -L$GLOBAL_OUTDIR/lib"
 
 export CXX="$DEVROOT/usr/bin/llvm-g++"
  export CC="$DEVROOT/usr/bin/llvm-gcc"

 export LD=$DEVROOT/usr/bin/ld
 export AR=$DEVROOT/usr/bin/ar
 export AS=$DEVROOT/usr/bin/as
 export NM=$DEVROOT/usr/bin/nm
 export RANLIB=$DEVROOT/usr/bin/ranlib
 export LDFLAGS="-L$SDKROOT/usr/lib/"
 
 export CPPFLAGS=$CFLAGS
 export CXXFLAGS=$CFLAGS
}

# build arm7
setenv_arm7()
{
 unset DEVROOT SDKROOT CFLAGS CC LD CPP CXX AR AS NM CXXCPP RANLIB LDFLAGS CPPFLAGS CXXFLAGS
 
   export DEVROOT=/Applications/Xcode.app/Contents/Developer/Platforms/iPhoneOS.platform/Developer
 export SDKROOT=$DEVROOT/SDKs/iPhoneOS$IOS_BASE_SDK.sdk
 
 export CFLAGS="-arch armv7 -pipe -no-cpp-precomp -isysroot $SDKROOT -miphoneos-version-min=$IOS_DEPLOY_TGT -I$SDKROOT/usr/include/"
 
 setenv_all
}

# build i386
setenv_i386()
{
 unset DEVROOT SDKROOT CFLAGS CC LD CPP CXX AR AS NM CXXCPP RANLIB LDFLAGS CPPFLAGS CXXFLAGS
 
 export DEVROOT=/Applications/Xcode.app/Contents/Developer/Platforms/iPhoneSimulator.platform/Developer
 export SDKROOT=$DEVROOT/SDKs/iPhoneSimulator$IOS_BASE_SDK.sdk
 
 export CFLAGS="-arch i386 -pipe -no-cpp-precomp -isysroot $SDKROOT -miphoneos-version-min=$IOS_DEPLOY_TGT"
 
 setenv_all
}

# create out direction
create_outdir_lipo()
{
 for lib_i386 in `find $LOCAL_OUTDIR/i386 -name "lib*\.a"`; do
  lib_arm7=`echo $lib_i386 | sed "s/i386/arm7/g"`
  lib=`echo $lib_i386 | sed "s/i386\///g"`
  lipo -arch armv7 $lib_arm7 -arch i386 $lib_i386 -create -output $lib
  done
}

# merge lib files
merge_libfiles()
{
 DIR=$1
 LIBNAME=$2
 
 cd $DIR
 for i in `find . -name "lib*.a"`; do
  $AR -x $i
 done
 $AR -r $LIBNAME *.o
 rm -rf *.o __*
 cd -
}

# download leptonica archive
get_leptonica()
{
    curl -C - -O http://www.leptonica.com/source/leptonica-1.69.tar.gz
    tar xzvf leptonica-1.69.tar.gz
}

# download tesseract archive
get_tessreact()
{
    curl -C - -O http://tesseract-ocr.googlecode.com/files/tesseract-3.01.tar.gz
    tar xzvf tesseract-3.01.tar.gz
}


#######################
# DOWNLOAD LIBS
#######################
# create out direction
mkdir $GLOBAL_OUTDIR
# download leptonica
get_leptonica 
# download tessreact 
get_tessreact 

#######################
# LEPTONLIB
#######################
cd $LEPTON_LIB
rm -rf $LOCAL_OUTDIR
mkdir -p $LOCAL_OUTDIR/arm7 $LOCAL_OUTDIR/i386

# armv7
make clean 2> /dev/null
make distclean 2> /dev/null
setenv_arm7
./configure --host=arm-apple-darwin6 --enable-shared=no --disable-programs --without-zlib --without-libpng --without-jpeg --without-giflib --without-libtiff || exit;
make -j4
cp -rvf src/.libs/lib*.a $LOCAL_OUTDIR/arm7

# i386
make clean 2> /dev/null
make distclean 2> /dev/null
setenv_i386
./configure --enable-shared=no --disable-programs --without-zlib --without-libpng --without-jpeg --without-giflib --without-libtiff || exit;
make -j4
cp -rvf src/.libs/lib*.a $LOCAL_OUTDIR/i386

create_outdir_lipo
mkdir -p $GLOBAL_OUTDIR/include/leptonica && cp -rvf src/*.h $GLOBAL_OUTDIR/include/leptonica
mkdir -p $GLOBAL_OUTDIR/lib && cp -rvf $LOCAL_OUTDIR/lib*.a $GLOBAL_OUTDIR/lib
cd ..


#######################
# TESSERACT-OCR (v3)
#######################
cd $TESSERACT_LIB
rm -rf $LOCAL_OUTDIR
mkdir -p $LOCAL_OUTDIR/arm7 $LOCAL_OUTDIR/i386

# armv7
make clean 2> /dev/null
make distclean 2> /dev/null
setenv_arm7
bash autogen.sh
./configure --host=arm-apple-darwin6 --enable-shared=no LIBLEPT_HEADERSDIR=$GLOBAL_OUTDIR/include/ || exit;
make -j4
for i in `find . -name "lib*.a"`; do cp -rvf $i $LOCAL_OUTDIR/arm7; done
merge_libfiles $LOCAL_OUTDIR/arm7 libtesseract_all.a

# i386
make clean 2> /dev/null
make distclean 2> /dev/null
setenv_i386
bash autogen.sh
./configure --enable-shared=no LIBLEPT_HEADERSDIR=$GLOBAL_OUTDIR/include/ || exit;
make -j4
for i in `find . -name "lib*.a" | grep -v arm`; do cp -rvf $i $LOCAL_OUTDIR/i386; done
merge_libfiles $LOCAL_OUTDIR/i386 libtesseract_all.a

create_outdir_lipo
mkdir -p $GLOBAL_OUTDIR/include/tesseract
tess_inc=( api/*.h ccmain/*.h ccstruct/*.h ccutil/*.h )
for i in "${tess_inc[@]}"; do
   cp -rvf $i $GLOBAL_OUTDIR/include/tesseract
done
mkdir -p $GLOBAL_OUTDIR/lib && cp -rvf $LOCAL_OUTDIR/lib*.a $GLOBAL_OUTDIR/lib
make clean 2> /dev/null
make distclean 2> /dev/null
rm -rf $LOCAL_OUTDIR
cd ..

echo "Finished!"

```