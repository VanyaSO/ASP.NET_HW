using hw_9_dop_2.Models;

public class ArticleService
{
    private List<Article> _articles = new List<Article>
    {
        new Article 
        { 
            Id = 1, 
            Title = "Основы тренировок для набора массы", 
            Description = "В этой статье рассматриваются основы эффективных тренировок для набора мышечной массы, включая важность прогрессивной нагрузки и правильного питания.",
            TextContent = "<p><strong>Набор мышечной массы</strong> — это сложный процесс, который включает в себя как тренировки, так и питание. Чтобы добиться успеха, необходимо соблюдать несколько принципов:</p><ul><li><strong>Прогрессивная нагрузка:</strong> увеличивайте веса и количество повторений на каждом занятии.</li><li><strong>Правильное питание:</strong> увеличение белка и углеводов в рационе способствует росту мышечной массы.</li><li><strong>Регулярность тренировок:</strong> стабильные тренировки 3-4 раза в неделю являются залогом роста мышц.</li></ul><p>Важно помнить, что тренировки должны быть сбалансированы с отдыхом. Мышцы растут именно в периоды восстановления после физических нагрузок.</p>",
            DateTimeCreate = new DateTime(2024, 11, 20), 
            CategoryId = 1 
        },
        new Article 
        { 
            Id = 2, 
            Title = "Программы питания для набора массы", 
            Description = "Данная статья освещает важность правильного питания для набора массы, включая рекомендации по увеличению калорийности и белка.",
            TextContent = "<p><strong>Питание играет ключевую роль в процессе набора массы</strong>. Недостаток калорий или неправильное соотношение макронутриентов может замедлить прогресс. Для эффективного набора мышечной массы важно учитывать несколько факторов:</p><ul><li><strong>Калорийность:</strong> увеличьте калорийность своего рациона, но избегайте избыточного потребления жиров.</li><li><strong>Белки:</strong> необходимы для восстановления и роста мышц. Рекомендуется потреблять около 2 г белка на килограмм массы тела.</li><li><strong>Углеводы:</strong> обеспечивают энергию для интенсивных тренировок, так что не пренебрегайте углеводами в своем рационе.</li></ul><p>Также важно соблюдать регулярность приемов пищи. Разделите их на 5-6 небольших приемов пищи, чтобы поддерживать стабильный уровень энергии в течение дня.</p>",
            Image = "/images/sklad.jpg",
            DateTimeCreate = new DateTime(2024, 11, 22), 
            CategoryId = 1 
        },
        new Article 
        { 
            Id = 3, 
            Title = "Тренировки с отягощениями", 
            Description = "В этой статье рассматриваются принципы эффективных тренировок с отягощениями для максимального роста мышц и силы.",
            TextContent = "<p><strong>Тренировки с отягощениями</strong> — это ключевой аспект набора мышечной массы. При правильном подходе они способствуют росту мышц, увеличению силы и выносливости. Чтобы получить максимальную отдачу от тренировок, стоит учитывать следующие моменты:</p><ul><li><strong>Используйте свободные веса:</strong> они задействуют больше мышц и способствуют их лучшему росту.</li><li><strong>Включите разнообразные упражнения:</strong> сочетайте упражнения на разные группы мышц, чтобы не допустить переутомления.</li><li><strong>Следите за техникой:</strong> неправильная техника может привести к травмам, поэтому всегда работайте с умеренными весами и обращайте внимание на каждое движение.</li></ul><p>Не забывайте, что восстановление также важно для роста мышц. После тренировок обязательно давайте своим мышцам время для восстановления.</p>",
            DateTimeCreate = new DateTime(2024, 11, 24), 
            CategoryId = 1 
        },
        new Article 
        { 
            Id = 4, 
            Title = "Как начать похудение", 
            Description = "Статья поможет вам разобраться с основами похудения, а также с важнейшими аспектами: снижением калорийности, регулярностью тренировок и питьевым режимом.",
            TextContent = "<p><strong>Похудение</strong> — это не только ограничение в еде, но и правильный подход к тренировкам и здоровому образу жизни. Чтобы начать путь к снижению веса, важно соблюдать несколько принципов:</p><ul><li><strong>Снизьте калорийность рациона:</strong> уменьшите потребление высококалорийных продуктов, таких как сладости и фастфуд.</li><li><strong>Регулярные тренировки:</strong> сочетание кардио и силовых упражнений способствует ускорению метаболизма и сжиганию жира.</li><li><strong>Питьевой режим:</strong> увеличение потребления воды способствует выведению токсинов из организма и улучшению обмена веществ.</li></ul><p>Не спешите и избегайте экстремальных диет — постепенное снижение веса безопасно для здоровья и помогает сохранить результаты в долгосрочной перспективе.</p>",
            DateTimeCreate = new DateTime(2024, 10, 18), 
            CategoryId = 2 
        },
        new Article 
        { 
            Id = 5, 
            Title = "Тренировки для сжигания жира", 
            Description = "В статье рассматриваются эффективные кардионагрузки для сжигания жира, включая бег, плавание и интервальные тренировки.",
            TextContent = "<p><strong>Кардионагрузки</strong> — это важнейший компонент эффективного сжигания жира. Они помогают ускорить обмен веществ и поддерживать высокий уровень энергии в организме. Рассмотрим несколько типов тренировок:</p><ul><li><strong>Бег:</strong> один из самых доступных и эффективных способов сжигать калории.</li><li><strong>Плавание:</strong> отличное кардио, которое помогает сжигать жир и развивает все группы мышц.</li><li><strong>Интервальные тренировки:</strong> высокоинтенсивные тренировки, чередующие периоды максимальной нагрузки и восстановления, эффективны для сжигания жира и ускорения метаболизма.</li></ul><p>Планируйте свои тренировки так, чтобы они занимали 30-60 минут, 3-4 раза в неделю.</p>",
            Image = "/images/sklad.jpg",
            DateTimeCreate = new DateTime(2024, 10, 21), 
            CategoryId = 2 
        },
        new Article 
        { 
            Id = 6, 
            Title = "Диета для похудения", 
            Description = "Эта статья охватывает основы правильного питания для похудения, акцентируя внимание на белках, углеводах и здоровых жирах.",
            TextContent = "<p><strong>Диета</strong> для похудения не должна быть строгой и голодной. Важно научиться контролировать размер порций и выбирать полезные продукты. Вот несколько рекомендаций:</p><ul><li><strong>Снижение калорий:</strong> уменьшайте потребление калорий с помощью более здоровых источников пищи — овощей, фруктов, белков и цельнозерновых продуктов.</li><li><strong>Регулярные приемы пищи:</strong> не пропускайте приемы пищи, чтобы избежать переедания в будущем.</li><li><strong>Питьевая вода:</strong> пьете достаточно воды, чтобы ускорить обмен веществ и предотвратить голод.</li></ul><p>Следите за тем, чтобы ваш рацион был сбалансированным и включал все необходимые питательные вещества, включая белки, углеводы и жиры.</p>",
            DateTimeCreate = new DateTime(2024, 10, 25), 
            CategoryId = 2 
        },
        new Article 
        { 
            Id = 7, 
            Title = "Физическая активность для людей старшего возраста", 
            Description = "В статье рассмотрены полезные виды физической активности для пожилых людей, включая йогу, плавание и ходьбу.",
            TextContent = "<p><strong>Для людей старше 60 лет</strong> важно поддерживать физическую активность, чтобы сохранять подвижность и здоровье. Даже легкие тренировки, такие как йога или плавание, могут значительно улучшить качество жизни. Рассмотрим несколько полезных упражнений:</p><ul><li><strong>Ходьба:</strong> обычная прогулка на свежем воздухе улучшает кровообращение и укрепляет сердце.</li><li><strong>Плавание:</strong> подходит для людей с проблемами суставов и является отличным способом поддержания физической формы.</li><li><strong>Йога:</strong> мягкие растяжки и дыхательные упражнения помогают укрепить мышцы и улучшить гибкость.</li></ul><p>Важным аспектом является регулярность, даже 20 минут в день могут дать значительные результаты.</p>", 
            DateTimeCreate = new DateTime(2024, 09, 25), 
            CategoryId = 3 
        },
        new Article 
        { 
            Id = 8, 
            Title = "Питание для людей старше 60 лет", 
            Description = "Статья обсуждает особенности питания для пожилых людей, акцентируя внимание на необходимых микроэлементах и витаминах.",
            TextContent = "<p><strong>Правильное питание</strong> — это основа здоровья для пожилых людей. С возрастом меняются потребности организма, и важно обращать внимание на несколько ключевых аспектов:</p><ul><li><strong>Кальций:</strong> необходим для укрепления костей и предотвращения остеопороза.</li><li><strong>Витамин D:</strong> важен для нормальной работы иммунной системы и поддержания костей.</li><li><strong>Овощи и фрукты:</strong> источники клетчатки и витаминов, которые поддерживают здоровье в пожилом возрасте.</li></ul><p>Не забывайте об умеренности в потреблении пищи, особенно в отношении соли и сахара, чтобы поддерживать оптимальное давление и уровень сахара в крови.</p>",
            DateTimeCreate = new DateTime(2024, 09, 28), 
            CategoryId = 3 
        }
    };


    public IEnumerable<Article> GetAllArticles() => _articles.ToList();

    public IEnumerable<Article> GetArticlesByCategoryId(int id) => _articles
        .Where(a => a.CategoryId == id)
        .ToList();

    public Article? GetArticleById(int id) => _articles.FirstOrDefault(a => a.Id == id);
}