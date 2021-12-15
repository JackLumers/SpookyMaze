# SpookyMaze
A demo game for my employment opportunity. Based on given task and technical specifications, with a portion of my own creative ideas (a.k.a programmer art, lol)

# Project installation

# Architecture
*On Russian 'cause fuck it and you'll probably will not have a good time translating it.*

- Логика реакций на события (открытие двери, определение того что игрок смотрит на дверь, запуск таймера и пр.) вдохновлена архитектурой через SO делегаты, которую Юнитеки показывали на своей конференции **[здесь](https://youtu.be/raQ3iHhE_Kk?t=1682)** (по таймкоду 28:00). Она изменена под нужды проекта, и также я не добавляю вызов функций через Unity Event в инспекторе, так мне удобнее смотреть что где вызывается из кода, а не инспектора.

- Поиск пути и передвижение ИксИна осуществяется через **NavMesh** компоненты от Unity.

- Передвижение игрока осуществляется через ассет **Third Person Character Controller** от Unity.

- Передвижение камеры осуществяется через пакет **Cinemachine**

- Логика двери реализована через встроенный в Unity **Animator** и строноннюю бибилотеку **UniTask** так как корутины медленнее и сложно расширяемы.

- Логика определения того на что смотрит игрок (для отрытия дверей) реализована через рассчет коллизии и вызов экшена в делегате, который написан через встроенный в Unity **Scriptable Object**. 

- Логика обработки ввода вывода (Input) реализована через **Input System** от Unity.

- UI реализован через встроенный в Unity **UI Module**.

- Логика контроллера игры реализована через... [дописать].

# Used Assets
- [Starter Assets - Third Person Character Controller](https://assetstore.unity.com/packages/essentials/starter-assets-third-person-character-controller-196526)

# Used Packages
- [Input System](https://docs.unity3d.com/Manual/com.unity.inputsystem.html)
- [UniTask](https://github.com/Cysharp/UniTask)
- [Cinemachine](https://docs.unity3d.com/Packages/com.unity.cinemachine%402.1/manual/index.html)
- [(Built-In) Animation Module](https://docs.unity3d.com/ScriptReference/UnityEngine.AnimationModule.html)
- [(Built-In) UI Module](https://docs.unity3d.com/ScriptReference/UnityEngine.UIModule.html)
- [(Built-In) Text Mesh Pro](https://docs.unity3d.com/Manual/com.unity.textmeshpro.html)
- [(Built-In) AI Module](https://docs.unity3d.com/Manual/com.unity.modules.ai.html)
