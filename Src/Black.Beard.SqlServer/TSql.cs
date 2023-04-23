using Bb.SqlServer.Asts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.SqlServer
{

    public enum UnitySizeEnum
    {
        Gb,
        Kb,
        Mb,
        Tb,
    }

    public static partial class TSql
    {

        public static AstDatabaseFile FileGroup(this string filegroupeName, params AstFileSpec[] files)
        {
            return AstDatabaseFile.New(AstFileGroup.New(filegroupeName, AstFileSpecs.New(files)));
        }

        public static AstDatabaseFile DatabaseFile(this string fileSpecName, string filename, decimal initSize, decimal maxSize, decimal sizeGrowth, UnitySizeEnum unity, UnitySizeEnum growUnity = UnitySizeEnum.Gb)
        {
            return AstDatabaseFile.New(FileSpec(fileSpecName, filename, initSize, maxSize, sizeGrowth, unity, growUnity));
        }

        public static AstFileSpec FileSpec(this string fileSpecName, string filename, decimal initSize, decimal maxSize, decimal sizeGrowth, UnitySizeEnum unity, UnitySizeEnum growUnity = UnitySizeEnum.Gb)
        {

            AstFileSizeUnity u1 = unity.Convert();
            AstFileSizeUnity u2 = growUnity.Convert();

            var result = AstFileSpec.New
            (
                AstNameSet.New(fileSpecName),
                AstFilenameSet.New(filename),
                AstSizeSet.New(AstFileSize.New(initSize, u1)),
                AstMaxsizeSet.New(AstMaxFileSizeValue.New(AstFileSize.New(maxSize, u1))),
                AstFilegrowth.New(AstFileSize.New(sizeGrowth, u2))
            );

            return result;

        }

        private static AstFileSizeUnity Convert(this UnitySizeEnum unity)
        {
            AstFileSizeUnity u = null;

            switch (unity)
            {
                case UnitySizeEnum.Kb:
                    u = AstFileSizeUnity.Kb();
                    break;
                case UnitySizeEnum.Mb:
                    u = AstFileSizeUnity.Mb();
                    break;
                case UnitySizeEnum.Tb:
                    u = AstFileSizeUnity.Tb();
                    break;

                case UnitySizeEnum.Gb:
                default:
                    u = AstFileSizeUnity.Gb();
                    break;
            }

            return u;
        }

    }


}
