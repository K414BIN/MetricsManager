Урок 3. Работа с базой данных.

                      Задание:

1) Добавьте логирование всех параметров в каждом из контроллеров в обоих проектах.
2) Добавьте репозитории для каждого типа метрик в сервис агент сбора метрик.
3) Добавьте тесты на все контроллеры и все методы с использованием заглушек.


 Выполнено на данный момент: 

а) установлены с помощью Nuget все требуемые пакеты;
б) написаны и исправлены классы из методички для встраиваемой СУБД SQLite;
в) протестировано в Postman`e корректная работа MetricsAgent c CpuMetricsController;
г) не удален метод [HttpGet("sql-test")] ;
д) созданы пространства имен MetricsAgent.DAL,MetricsAgent.Models, MetricsAgent.Responses и MetricsAgent.Requests;
е) создан и протестирован Moq-тест для CpuMetricsController;
ё) создано и протестировано логирование для CpuMetricsController;
ж) создан и добавлен в решение новый проект MainLibrary, который содержит общие файлы ресурсов, классов, интерфейсов и пр.

 Выполнено на данный момент: 

a) удален метод [HttpGet("sql-read-write-test")];
б) добавлено логирование везде;
в) отвалились тесты для MetricsManager;
г) не смог сделать правильно метод Update в  NetworkMetricsController и  CpuMetricsController;
д) для контроллеров  HddMetricsController, RamMetricsController и  DotNetMetricsController в проекте MetricsAgent не написаны требуемые методы.