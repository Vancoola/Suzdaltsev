Была разработана модель пункта меню. Меню строится на указании родительского элемента. В строку URL может быть внесено url явным способом или использоваться name из urls.py (Требуется указать!). В модель встроен валидатор для name ссылок.
Templates тэг формирует html верстку из одного запроса, рекурсивно перебирая каждый элемент объекта. Для рисовки меню в index.html требуется указать {% draw_menu ‘name’ %}, где name название родительского пункта. 
В url можно указать название пункта меню, который будет показан.  
