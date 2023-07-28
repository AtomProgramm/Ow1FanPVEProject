# VersionCode 0
---
# На русском (In Russian)
 
## Основное описание
**Этот проект должен был представлять собой проект Unity C# для создания фанатского PVE  миссии от фаната overwatch1.** Для реализации идеи необходимо переписывать и рефакторить проект полность, как минимум многое переписать под паттерн StateMachine и продолжить реализацию до конца.
Большинство планов на реализацию было описанно в файле "plan" в Assets проекта.
Дополнительно хочу заметить и оправдатся что проект писался и недописался в короткие сроки, планировался ка быстрый эксперемент.
**Работа над этим репозиторием стоит под большим вопросом зависящим от целесообразности, состояния и возможностей автора, рекации людей. Однако если вдруг вы энтузиаст то всё в ваших руках.**

## Содействие
Если вам понравилась идея проекта, можете начать его переписывать в целях создать законченный продукт или же в учебных целях. Но первый вариант **рекомендуется начинать не ранее сентября 2023г. ввиду решения судьбы этого репзитория.** Автор посторается к сентябрю решить судьбу проэкта и начать переписывать, если это случится то с коммитом будет изменён README с подробностями процесса поддержки планами и т. д. (Если работать совсем не втерпёж можете попробовать связатся с автором)
В случае если вы читаете это в сентябре или после при этом хотите реализовывать этот проект, вот вам рекомендации:
1. Напишите DesignDoc 
    Одна если не главная причина превращения ппроекта спагети не поддрживаемый код, который надо переписывать и или рефакторить. Так как из-за обстоятельств предпологалось приключение на 20 мин.
    Совету прописать по очереди в файлик, можно даже в README:
        -Термины и сокращения используемыее далее по документу
        -Система управления
        -Системы и механики
        -Параметры (значения хила, урона ...)
        -Арзитиктура классов
2. Разберитесь с анимациями
    В процессе я понял что большенство логики можно и лучше сделать на event-ах анимации однако экспортировать их из overwotch не получилось (Меш превращяется в кашу и анимация читается как 3768 fps) + сделать анимируованные руки для первого лица тоже не вышло. А анимировать с нуля небыло не времени ни навыков не сил.
3. Напишите/перепишите проектат
    Тут сам процесс который зависит от ваших знаний и умений.
4. Попробуйте выпустить
    Можете попытать счасте и сделать pull request, если автор сможет он его примет. Или просто развивать новый репозиторий на github? всё в ваших руках. Удачи в разработке
  
## Контактная информация
Если вдруг нужно. Email: mishgeorgi@gmail.com
## Что в уже существующем проекте?
### Ассеты
- Heroes and Enemys 
Папка со всеми ассетами героев, игроков, вргов(ботов) и дополнительными к ним
- Lvls
Папка с ассетами левел дизайна. Например заложник, захватываемые точки, спавнеры врагов, триггеры...
### ~~Описание скриптов и префабов~~

У меня кочились силы писать, и нет времени. Извините.

----     
# In English
## Basic Description
**This project was supposed to be a Unity C# project to create a fan PVE mission from an overwatch1 fan.** To implement the idea, it is necessary to rewrite and refactor the project completely, at least rewrite a lot under the StateMachine pattern and continue the implementation to the end.
Most of the implementation plans were described in the "plan" file in the Assets project.
Additionally, I want to note and justify that the project was written and underwritten in a short time, a quick experiment was planned.
**The work on this repository is under a big question depending on the expediency, the state and capabilities of the author, the motivation of people. However, if suddenly you are an enthusiast, then everything is in your hands.**

## Assistance
If you liked the idea of the project, you can start rewriting it in order to create a finished product or for educational purposes. But the first option ** is recommended to start no earlier than September 2023. due to the decision of the fate of this repository.** The author will try to decide the fate of the project by September and start rewriting, if this happens, the README will be changed with the commit with details of the support process, plans, etc. (If you can't wait to work at all, you can try to contact the author)
If you read this in September or after that you want to implement this project, here you are recommendations:
1. Write a DesignDoc 
    One, if not the main reason for the transformation of the spaghetti project is not supported code that needs to be rewritten and or refactored. Because of the circumstances, an adventure for 20 minutes was expected.
    The advice should be written in turn in the file, you can even in the README:
-Terms and abbreviations are used further on in the document
        -Control system
        -Systems and mechanics
        -Parameters (values of heal, damage...)
-Class architecture
2. Figure out the animations
    In the process, I realized that most of the logic can be done better on event animations, but it didn't work out to export them from overwotch (the mesh turns into porridge and the animation reads like 3768 fps) + it didn't work out to make animated hands for the first person either. And there was no time to animate from scratch, no skills, no strength.
3. Write/rewrite the project
    Here is the process itself, which depends on your knowledge and skills.
4. Try to release
    You can try your luck and make a pull request, if the author can, he will accept it. Or just develop a new repository on github? everything is in your hands. Good luck in development
  
## Contact information
If you suddenly need to. Email: mishgeorgi@gmail.com
## What's in an existing project?
### Assets
- Heroes and Enemys 
Folder with all assets of heroes, players, vrgs (bots) and additional to them
- Lvls
Folder with level design assets. For example hostage, captured points, enemy spawners, triggers...
### ~~Description of scripts and prefabs~~

I've run out of energy to write, and I don't have time. Sorry.