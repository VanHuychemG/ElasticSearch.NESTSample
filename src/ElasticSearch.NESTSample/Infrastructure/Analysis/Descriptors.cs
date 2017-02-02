using Nest;

namespace ElasticSearch.NESTSample.Infrastructure.Analysis
{
    public class Descriptors
    {
        public static AnalysisDescriptor DutchAnalysis(AnalysisDescriptor analysis) => analysis

            //  custom filters
            .TokenFilters(tokenfilters => tokenfilters
                .Stop("dutch_stop", w => w
                    .StopWords("_dutch_")
                )
                .Stemmer("dutch_stemmer", w => w
                    .Language("dutch")
                )
            )
            .CharFilters(charFilters => charFilters
                .PatternReplace("kill_numbers", p => p
                    .Pattern("(\\d+)")
                    .Replacement("")))

            //  custom analyzers
            .Analyzers(analyzers => analyzers
                .Custom("dutch", c => c
                    .CharFilters("kill_numbers")
                    .Tokenizer("standard")
                    .Filters("lowercase", "dutch_stop", "dutch_stemmer")
                )
            );

        public static AnalysisDescriptor FrenchAnalysis(AnalysisDescriptor analysis) => analysis

            //  custom filters
            .TokenFilters(tokenfilters => tokenfilters
                .Stop("french_stop", w => w
                    .StopWords("_french_")
                )
                .Stemmer("french_stemmer", w => w
                    .Language("french")
                )
            )
            .CharFilters(charFilters => charFilters
                .PatternReplace("kill_numbers", p => p
                    .Pattern("(\\d+)")
                    .Replacement("")))

            //  custom analyzers
            .Analyzers(analyzers => analyzers
                .Custom("french", c => c
                    .CharFilters("kill_numbers")
                    .Tokenizer("standard")
                    .Filters("elision", "lowercase", "french_stop", "french_stemmer")
                )
            );
    }
}
