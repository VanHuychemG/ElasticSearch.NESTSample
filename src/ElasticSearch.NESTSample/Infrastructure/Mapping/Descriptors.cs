using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticSearch.NESTSample.Domain;
using Nest;

namespace ElasticSearch.NESTSample.Infrastructure.Mapping
{
    public class Descriptors
    {
        public static TypeMappingDescriptor<NewsItem> DutchMappings(TypeMappingDescriptor<NewsItem> map) => map

           .Properties(ps => ps

               .Text(t => t
                   .Name(p => p.Id)
                   .Analyzer("keyword"))

               .Date(d => d
                   .Name(p => p.PublishDateTime)
                   .Format("yyyy-MM-dd HH:mm:ss||yyyy-MM-dd||epoch_millis"))

               .Date(d => d
                   .Name(p => p.PublishDate)
                   .Format("yyyy-MM-dd HH:mm:ss||yyyy-MM-dd||epoch_millis"))

               .Text(t => t
                   .Name(p => p.Title)
                   .Analyzer("dutch")
                   .TermVector(TermVectorOption.WithPositionsOffsetsPayloads)
               )

               .Text(t => t
                   .Name(p => p.Lead)
                   .Analyzer("dutch")
                   .TermVector(TermVectorOption.WithPositionsOffsetsPayloads)
               )

               .Text(t => t
                   .Name(p => p.Body)
                   .Analyzer("dutch")
                   .TermVector(TermVectorOption.WithPositionsOffsetsPayloads)
               )

               .Text(t => t
                   .Name(p => p.Language)
                   .Analyzer("keyword"))

                .Nested<Author>(x => x
                    .Name(y => y.Authors)
                    .IncludeInRoot()
                    .Properties(z => z
                        .Keyword(a => a
                            .Name(b => b.Firstname))
                        .Keyword(a => a
                            .Name(b => b.Lastname))))
           );

        public static TypeMappingDescriptor<NewsItem> FrenchMappings(TypeMappingDescriptor<NewsItem> map) => map

            .Properties(ps => ps

                .Text(t => t
                    .Name(p => p.Id)
                    .Analyzer("keyword"))

                .Date(d => d
                    .Name(p => p.PublishDateTime)
                    .Format("yyyy-MM-dd HH:mm:ss||yyyy-MM-dd||epoch_millis"))

                .Date(d => d
                    .Name(p => p.PublishDate)
                    .Format("yyyy-MM-dd HH:mm:ss||yyyy-MM-dd||epoch_millis"))

                .Text(t => t
                    .Name(p => p.Title)
                    .Analyzer("french")
                    .TermVector(TermVectorOption.WithPositionsOffsetsPayloads)
                )

                .Text(t => t
                    .Name(p => p.Lead)
                    .Analyzer("french")
                    .TermVector(TermVectorOption.WithPositionsOffsetsPayloads)
                )

                .Text(t => t
                    .Name(p => p.Body)
                    .Analyzer("french")
                    .TermVector(TermVectorOption.WithPositionsOffsetsPayloads)
                )

                .Text(t => t
                    .Name(p => p.Language)
                    .Analyzer("keyword"))

                .Nested<Author>(x => x
                    .Name(y => y.Authors)
                    .IncludeInRoot()
                    .Properties(z => z
                        .Keyword(a => a
                            .Name(b => b.Firstname))
                        .Keyword(a => a
                            .Name(b => b.Lastname))))
            );
    }
}
