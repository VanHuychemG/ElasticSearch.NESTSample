using System;
using System.Collections.Generic;

using ElasticSearch.NESTSample.Domain;
using ElasticSearch.NESTSample.Infrastructure.Configuration;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ElasticSearch.NESTSample.Infrastructure
{
    public interface IReader
    {
        IList<NewsItem> Read();
    }

    public class Reader : IReader
    {
        private readonly ILogger<Reader> _logger;
        private readonly IndexConfiguration _configuration;

        public Reader(
            ILogger<Reader> logger,
            IOptions<IndexConfiguration> configuration)
        {
            _logger = logger;
            _configuration = configuration.Value;
        }

        public IList<NewsItem> Read()
        {
            _logger.LogInformation("Started Reading");

            var newsItems = new List<NewsItem>
            {
                new NewsItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Wat is Lorem Ipsum?",
                    Lead = "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.",
                    Body = "In tegenstelling tot wat algemeen aangenomen wordt is Lorem Ipsum niet zomaar willekeurige tekst. het heeft zijn wortels in een stuk klassieke latijnse literatuur uit 45 v.Chr. en is dus meer dan 2000 jaar oud. Richard McClintock, een professor latijn aan de Hampden-Sydney College in Virginia, heeft één van de meer obscure latijnse woorden, consectetur, uit een Lorem Ipsum passage opgezocht, en heeft tijdens het zoeken naar het woord in de klassieke literatuur de onverdachte bron ontdekt. Lorem Ipsum komt uit de secties 1.10.32 en 1.10.33 van \"de Finibus Bonorum et Malorum\" (De uitersten van goed en kwaad) door Cicero, geschreven in 45 v.Chr. Dit boek is een verhandeling over de theorie der ethiek, erg populair tijdens de renaissance. De eerste regel van Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", komt uit een zin in sectie 1.10.32. Het standaard stuk van Lorum Ipsum wat sinds de 16e eeuw wordt gebruikt is hieronder, voor wie er interesse in heeft, weergegeven. Secties 1.10.32 en 1.10.33 van \"de Finibus Bonorum et Malorum\" door Cicero zijn ook weergegeven in hun exacte originele vorm, vergezeld van engelse versies van de 1914 vertaling door H. Rackham.",
                    Language = "nl",
                    PublishDate = DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                    PublishDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Authors = new List<Author>
                    {
                        new Author { Id = Guid.NewGuid(), Firstname = "John", Lastname = "Doe"},
                        new Author { Id = Guid.NewGuid(),Firstname = "Jane", Lastname = "Doe"}
                    },
                    TargetIndex = $"{_configuration.LiveIndexAlias}-nl-{DateTime.Now.Date:yyyy-MM-dd}"
                },
                new NewsItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Qu'est-ce que le Lorem Ipsum?",
                    Lead = "Le Lorem Ipsum est simplement du faux texte employé dans la composition et la mise en page avant impression. Le Lorem Ipsum est le faux texte standard de l'imprimerie depuis les années 1500, quand un peintre anonyme assembla ensemble des morceaux de texte pour réaliser un livre spécimen de polices de texte. Il n'a pas fait que survivre cinq siècles, mais s'est aussi adapté à la bureautique informatique, sans que son contenu n'en soit modifié. Il a été popularisé dans les années 1960 grâce à la vente de feuilles Letraset contenant des passages du Lorem Ipsum, et, plus récemment, par son inclusion dans des applications de mise en page de texte, comme Aldus PageMaker.",
                    Body = "Contrairement à une opinion répandue, le Lorem Ipsum n'est pas simplement du texte aléatoire. Il trouve ses racines dans une oeuvre de la littérature latine classique datant de 45 av. J.-C., le rendant vieux de 2000 ans. Un professeur du Hampden-Sydney College, en Virginie, s'est intéressé à un des mots latins les plus obscurs, consectetur, extrait d'un passage du Lorem Ipsum, et en étudiant tous les usages de ce mot dans la littérature classique, découvrit la source incontestable du Lorem Ipsum. Il provient en fait des sections 1.10.32 et 1.10.33 du \"De Finibus Bonorum et Malorum\" (Des Suprêmes Biens et des Suprêmes Maux) de Cicéron. Cet ouvrage, très populaire pendant la Renaissance, est un traité sur la théorie de l'éthique. Les premières lignes du Lorem Ipsum, \"Lorem ipsum dolor sit amet...\", proviennent de la section 1.10.32. L'extrait standard de Lorem Ipsum utilisé depuis le XVIè siècle est reproduit ci-dessous pour les curieux. Les sections 1.10.32 et 1.10.33 du \"De Finibus Bonorum et Malorum\" de Cicéron sont aussi reproduites dans leur version originale, accompagnée de la traduction anglaise de H. Rackham (1914).",
                    Language = "fr",
                    PublishDate = DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                    PublishDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Authors = new List<Author>
                    {
                        new Author { Id = Guid.NewGuid(),Firstname = "Jack", Lastname = "Doe"},
                        new Author { Id = Guid.NewGuid(),Firstname = "Jill", Lastname = "Doe"}
                    },
                    TargetIndex = $"{_configuration.LiveIndexAlias}-fr-{DateTime.Now.Date:yyyy-MM-dd}"
                },
                new NewsItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Qu'est-ce que le Lorem Ipsum?",
                    Lead = "Le Lorem Ipsum est simplement du faux texte employé dans la composition et la mise en page avant impression. Le Lorem Ipsum est le faux texte standard de l'imprimerie depuis les années 1500, quand un peintre anonyme assembla ensemble des morceaux de texte pour réaliser un livre spécimen de polices de texte. Il n'a pas fait que survivre cinq siècles, mais s'est aussi adapté à la bureautique informatique, sans que son contenu n'en soit modifié. Il a été popularisé dans les années 1960 grâce à la vente de feuilles Letraset contenant des passages du Lorem Ipsum, et, plus récemment, par son inclusion dans des applications de mise en page de texte, comme Aldus PageMaker.",
                    Body = "Contrairement à une opinion répandue, le Lorem Ipsum n'est pas simplement du texte aléatoire. Il trouve ses racines dans une oeuvre de la littérature latine classique datant de 45 av. J.-C., le rendant vieux de 2000 ans. Un professeur du Hampden-Sydney College, en Virginie, s'est intéressé à un des mots latins les plus obscurs, consectetur, extrait d'un passage du Lorem Ipsum, et en étudiant tous les usages de ce mot dans la littérature classique, découvrit la source incontestable du Lorem Ipsum. Il provient en fait des sections 1.10.32 et 1.10.33 du \"De Finibus Bonorum et Malorum\" (Des Suprêmes Biens et des Suprêmes Maux) de Cicéron. Cet ouvrage, très populaire pendant la Renaissance, est un traité sur la théorie de l'éthique. Les premières lignes du Lorem Ipsum, \"Lorem ipsum dolor sit amet...\", proviennent de la section 1.10.32. L'extrait standard de Lorem Ipsum utilisé depuis le XVIè siècle est reproduit ci-dessous pour les curieux. Les sections 1.10.32 et 1.10.33 du \"De Finibus Bonorum et Malorum\" de Cicéron sont aussi reproduites dans leur version originale, accompagnée de la traduction anglaise de H. Rackham (1914).",
                    Language = "fr",
                    PublishDate = DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                    PublishDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    TargetIndex = $"{_configuration.LiveIndexAlias}-fr-{DateTime.Now.Date:yyyy-MM-dd}"
                }
            };

            _logger.LogInformation("Stopped Reading");

            return newsItems;
            ;
        }
    }
}
