// // Probe.Run();

// Guild guild = new Guild("Гильдия героев");
// Hero sergey = new Hero("Сергей", 110);
// Hero yaroslav = new Hero("Ярослав", 100);
// Hero elena = new Hero("Елена", 100);
// Hero artem = new Hero("Артём", 100);
// Hero anastasia = new Hero("Анастасия", 100);

// guild.AddHero(sergey);
// guild.AddHero(yaroslav);
// guild.AddHero(elena);
// guild.AddHero(artem);
// guild.AddHero(anastasia);

// Console.WriteLine($">>> {guild.Name}: участников {guild.GetMembers().Count} <<<");
// Console.WriteLine();

// QuestBoard board = new QuestBoard(guild);

// board.AddQuest(new Quest(101, "Вернуть поплавок", 100, 30));
// board.TryAssign(101, "Сергей");
// board.TryComplete(101);

// board.AddQuest(new Quest(102, "Проверить нижнюю площадку", 100, 10));
// board.AddQuest(new Quest(103, "Снять мерки для насоса больницы", 100, 20));
// board.AddQuest(new Quest(104, "Обследовать ответвление", 100, 15));

// Console.WriteLine($"Сергей на №102: {board.TryAssign(102, "Сергей")}");
// Console.WriteLine($"Сергей на №103: {board.TryAssign(103, "Сергей")}");
// Console.WriteLine($"Отмена №102: {board.TryCancel(102)}");
// Console.WriteLine($"Сергей на №103: {board.TryAssign(103, "Сергей")}");
// Console.WriteLine($"Завершение №103: {board.TryComplete(103)}");
// Console.WriteLine($"Ярослав на №104: {board.TryAssign(104, "Ярослав")}");
// Console.WriteLine($"Удалить Сергея: {board.TryRemoveMember("Сергей")}");

// Console.WriteLine();
// List<Quest> quests = board.GetQuests();
// foreach (Quest quest in quests) {
//     Console.WriteLine(quest.GetInfo());
// }

// Console.WriteLine($"Открыто: {board.CountByStatus(QuestStatus.Open)}, " +
//                   $"назначено: {board.CountByStatus(QuestStatus.Assigned)}, " +
//                   $"выполнено: {board.CountByStatus(QuestStatus.Completed)}, " +
//                   $"отменено: {board.CountByStatus(QuestStatus.Cancelled)}");

// Quest? yaroslavQuest = board.FindActiveQuest("Ярослав");
// if (yaroslavQuest != null) {
//     Console.WriteLine($"Ярослав сейчас: №{yaroslavQuest.Id} «{yaroslavQuest.Title}»");
// }

// Quest? sergeyQuest = board.FindActiveQuest("Сергей");
// if (sergeyQuest == null) {
//     Console.WriteLine("Сергей сейчас: свободен");
// }

// Console.WriteLine();
// Console.WriteLine(sergey.GetInfo());

GameCatalog catalog = new GameCatalog("Каталог Игр");

Game persona = new Game("Persona 3 Reload", "JRPG", 2024, 10.0);
Game zelda = new Game("The Legend Of Zelda: Breath Of The Wild", "Adventure", 2017, 10.0);
Game cyberpunk = new Game("Cyberpunk 2077", "RPG", 2020, 8.5);

catalog.AddGame(persona);
catalog.AddGame(zelda);
catalog.AddGame(cyberpunk);

ReviewBoard reviewBoard = new ReviewBoard(catalog);

reviewBoard.AddSession(new ReviewSession(1, 2018, 9.0));
reviewBoard.AddSession(new ReviewSession(2, 2018, 9.5));
reviewBoard.AddSession(new ReviewSession(3, 2020, 9.8));

Console.WriteLine($"Старт сессии №1 на Cyberpunk 2077: {reviewBoard.TryStart(1, "Cyberpunk 2077")}");
Console.WriteLine($"Старт сессии №2 на Cyberpunk 2077: {reviewBoard.TryStart(2, "Cyberpunk 2077")}");
Console.WriteLine($"Отмена сессии №1: {reviewBoard.TryCancel(1)}");
Console.WriteLine($"Повторный старт сессии №2 на Cyberpunk 2077: {reviewBoard.TryStart(2, "Cyberpunk 2077")}");
Console.WriteLine($"Завершение сессии №2: {reviewBoard.TryFinish(2)}");
Console.WriteLine($"Старт сессии №3 на Persona 3 Reload: {reviewBoard.TryStart(3, "Persona 3 Reload")}");
Console.WriteLine($"Удалить Cyberpunk 2077: {reviewBoard.TryRemoveGame("Cyberpunk 2077")}");

Console.WriteLine();
foreach (ReviewSession s in reviewBoard.GetSessions()) {
    Console.WriteLine(s.GetInfo());
}

Console.WriteLine($"Открыто: {reviewBoard.CountByStatus(ReviewStatus.Open)}, " +
                  $"в работе: {reviewBoard.CountByStatus(ReviewStatus.InProgress)}, " +
                  $"завершено: {reviewBoard.CountByStatus(ReviewStatus.Completed)}, " +
                  $"отменено: {reviewBoard.CountByStatus(ReviewStatus.Cancelled)}");

Console.WriteLine();
Game? updatedGame = catalog.FindGameByTitle("cyberpunk 2077");
if (updatedGame != null) {
    Console.WriteLine($"Проверка в каталоге: {updatedGame.Title}, актуальный рейтинг: {updatedGame.Rating}");
}