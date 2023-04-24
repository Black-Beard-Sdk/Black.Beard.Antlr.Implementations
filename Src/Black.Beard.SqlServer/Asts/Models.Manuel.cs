#nullable disable

namespace Bb.SqlServer.Asts
{
    using System;
    using Bb.Parsers;
    using Antlr4.Runtime;
    using Antlr4.Runtime.Tree;


    public partial class AstOnOff : AstTerminalKeyword
    {

        public static implicit operator AstOnOff(bool value)
        {
            if (value)
                return AstOnOff.On();
            return AstOnOff.Off();
        }

    }


    public partial class AstEnableDisable : AstTerminalKeyword
    {

        public static implicit operator AstEnableDisable(bool value)
        {
            if (value)
                return AstEnableDisable.Enable();
            return AstEnableDisable.Disable();
        }

    }

    public abstract partial class AstDatabaseFile : AstBnfRule
    {

        public static AstDatabaseFile FileGroup(string filegroupeName, params AstFileSpec[] files)
        {
            return AstDatabaseFile.New(AstFileGroup.New(filegroupeName, AstFileSpecs.New(files)));
        }

        public static AstDatabaseFile File(string fileSpecName, string filename, decimal initSize, UnitySizeEnum initSizeUnity, decimal sizeGrowth, UnitySizeEnum growUnity)
        {
            return AstDatabaseFile.New(AstFileSpec.File(fileSpecName, filename, initSize, initSizeUnity, sizeGrowth, growUnity));
        }

        public static AstDatabaseFile File(string fileSpecName, string filename, decimal initSize, UnitySizeEnum initSizeUnity, decimal maxSize, UnitySizeEnum maxSizeUnity, decimal sizeGrowth, UnitySizeEnum growUnity)
        {
            return AstDatabaseFile.New(AstFileSpec.File(fileSpecName, filename, initSize, initSizeUnity, maxSize, maxSizeUnity, sizeGrowth, growUnity));
        }

    }


    public abstract partial class AstCreateDatabaseOption : AstBnfRule
    {

        public static AstCreateDatabaseOption FileStream(AstDatabaseFilestreamOption item1)
        {
            return AstCreateDatabaseOption.New(AstDatabaseFilestreamOptions.New(item1));
        }

        public static AstCreateDatabaseOption FileStream(AstDatabaseFilestreamOption item1, AstDatabaseFilestreamOption item2)
        {
            return AstCreateDatabaseOption.New(AstDatabaseFilestreamOptions.New(item1, item2));
        }

        public static AstCreateDatabaseOption DbChaining(bool value)
        {
            return AstCreateDatabaseOption.New(AstDbChainingSet.New(value));
        }

        public static AstCreateDatabaseOption Trustworthy(bool value)
        {
            return AstCreateDatabaseOption.New(AstTrustworthySet.New(value));
        }

        public static AstCreateDatabaseOption DefaultLanguage(decimal value)
        {
            return AstCreateDatabaseOption.New(AstDefaultLanguageSet.New(AstLanguageSettingValue.New(value)));
        }

        public static AstCreateDatabaseOption DefaultLanguage(string value)
        {
            return AstCreateDatabaseOption.New(AstDefaultLanguageSet.New(AstLanguageSettingValue.New(AstLanguageId.New(value))));
        }

        public static AstCreateDatabaseOption DefaultFulltextLanguage(string language)
        {
            return AstCreateDatabaseOption.New(AstDefaultFulltextLanguageSet.New(AstLanguageSetting.New(language)));
        }

        public static AstCreateDatabaseOption NestedTriggers(bool value)
        {
            return AstCreateDatabaseOption.New(AstNestedTriggersSet.New(value));
        }

        public static AstCreateDatabaseOption TransformNoiseWords(bool value)
        {
            return AstCreateDatabaseOption.New(AstTransformNoiseWordsSet.New(value));
        }

        public static AstCreateDatabaseOption TwoDigitYearCutoff(decimal value)
        {
            return AstCreateDatabaseOption.New(AstTwoDigitYearCutoffSet.New(value));
        }

    }

    public partial class AstFileSpec : AstBnfRule
    {

        public static AstFileSpec File(string fileSpecName, string filename, decimal initSize, UnitySizeEnum initSizeUnity, decimal sizeGrowth, UnitySizeEnum growUnity)
        {

            var result = AstFileSpec.New
            (
                AstNameSet.New(fileSpecName),
                AstFilenameSet.New(filename),
                AstSizeSet.New(AstFileSize.New(initSize, initSizeUnity)),
                AstMaxsizeSet.New(new AstMaxFileSizeValue.AstMaxFileSizeValue2()),
                AstFilegrowthSet.New(AstFileSize.New(sizeGrowth, growUnity))
            );

            return result;

        }

        public static AstFileSpec File(string fileSpecName, string filename, decimal initSize, UnitySizeEnum initSizeUnity, decimal maxSize, UnitySizeEnum maxSizeUnity, decimal sizeGrowth, UnitySizeEnum growUnity)
        {

            var result = AstFileSpec.New
            (
                AstNameSet.New(fileSpecName),
                AstFilenameSet.New(filename),
                AstSizeSet.New(AstFileSize.New(initSize, initSizeUnity)),
                AstMaxsizeSet.New(AstMaxFileSizeValue.New(AstFileSize.New(maxSize, maxSizeUnity))),
                AstFilegrowthSet.New(AstFileSize.New(sizeGrowth, growUnity))
            );

            return result;

        }

    }

    public partial class AstFileSizeUnity : AstTerminalKeyword
    {

        public static implicit operator AstFileSizeUnity(UnitySizeEnum unity)
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


    public abstract partial class AstDatabaseFilestreamOption : AstBnfRule
    {

        public static AstDatabaseFilestreamOption DirectoryName(string directoryName)
        {
            return AstDatabaseFilestreamOption.New(AstDirectoryNameSet.New(directoryName));
        }

        public static AstDatabaseFilestreamOption NonTransactedAccess(NonTransactedAccessEnum value)
        {
            return AstDatabaseFilestreamOption.New(AstNonTransactedAccessSet.New(value));
        }

    }

    public partial class AstOffReadOnlyFull : AstTerminalKeyword
    {

        public static implicit operator AstOffReadOnlyFull(NonTransactedAccessEnum value)
        {
            switch (value)
            {
                case NonTransactedAccessEnum.ReadOnly:
                    return AstOffReadOnlyFull.ReadOnly();

                case NonTransactedAccessEnum.Full:
                    return AstOffReadOnlyFull.Full();

                default:
                    break;

            }

            return AstOffReadOnlyFull.Off();

        }

    }

    public enum NonTransactedAccessEnum
    {
        Off,
        ReadOnly,
        Full
    }

    public enum UnitySizeEnum
    {
        Gb,
        Kb,
        Mb,
        Tb,
    }

}
