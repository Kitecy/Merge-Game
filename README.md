## Техническое задание "Merge Game" [RU]

Реализованные механики:

1. Drag & Drop механика с возможностью добавления любому UI объекту перемещения на экране и также любого UI объекта указанием как места, куда передвигаемый объект можно положить
2. Merge механика, при которой можно использовать как базовую систему BaseMergeable, так и своё уникальное и необходимое решение с использованием IMergeable. В данном варианте была реализована система основанная на использовании пулов. Также возможен был вариант с отдельным хранилищем всех уровней и соотстветсвующих им спрайтов и в последствии изменение на 1 объекте этого спрайта, но в рамках небольшой задачи решил оставить систему пулов. Текущую систему можно быстро модернизировать до озвученной
3. Присутствует возможность изменения доски и создания разных видов досок для разных уровней. Вариант с её генерацией через параметры при загрузке сцены был опущен, так как это тоже показалось излишней функциональностью в небольшом тестовом проекте.

Использованные пакеты:

1. UniTask
2. DOTween
3. [Animation Players](https://github.com/Kitecy/Animation-players/tree/main)

## Terms of reference "Merge Game" [EN]

Implemented mechanics:

1. Drag & Drop mechanics with the ability to add any UI object to move on the screen, as well as any UI object indicating where the object being moved can be placed
2. Merge mechanics, in which you can use both the basic BaseMergeable system and your own unique and necessary solution using IMergeable. In this version, a system based on the use of pools was implemented. It was also possible to have a separate storage of all levels and their corresponding sprites and subsequently change this sprite to 1 object, but as part of a small task, I decided to leave the pool system. The current system can be quickly upgraded to the announced one.
3. It is possible to change the board and create different types of boards for different levels. The option of generating it through the parameters when loading the scene was omitted, as this also seemed like unnecessary functionality in a small test project.

Used packages:

1. UniTask
2. DOTween
3. [Animation Players](https://github.com/Kitecy/Animation-players/tree/main)
