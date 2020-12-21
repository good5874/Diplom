# Diplom
Мини гайд как юзать эту штуку:
    
    1) Как работает основная механика выполнения заказа.
        
        Войти в  систему или зарегать свой ак, если создавать новый то в личном кабинете в профиле заполнить поля.
        Дальше перейти в личном кабинете в my cargo там создать груз.
        Перейти в личном кабинете my orders создать там заказ.
        
        Дальше должен войти менеджер, в кабинет в store orders забрать заказ. В store orders видны все новые заказы всем менеджерам.
        Этот заказ появится у менеджера в кабинете в my orders. Дальше менеджер должен указать в my orders-----> set cost цену.
        
        Дальше заказчик входит в кабинет в my orders ---> details ----> pay, кнопка имитирует оплату))
        
        Дальше должен войти менеджер, в кабинет в my orders, должен указать в my orders-----> details ------> appoint executors водителя и машину которые будут выполнять заказ.
        
        теперь у водителя в кабинете будет отображаться заказ в my orders, как он выполнит его жмет report и вводит свои расходы.
        
        заказ выполнен)
        
     2) Как работает основная механика добавления новых раотников заказа        
        Чтобы в организацию добавить водителя или менеджера, новый пользователь в личном кабинете кликает job далее выбирает желаемое и заполняет поля.
        Как заполнит, у админа в organization ---> vacancies отображаются пользователи которые заполнили формы вакансии, далее во вкладке roles нужно добавить акаунту роль.
        Новый работник готов)))
        P.S За админа также можно добавлять другие организаци и прикреплять к организациям машины. Также посмотреть прибыль по заказам и заказы которые выполняются.

юзеры которые создаются вместе с базой:

            string adminEmail = "admin1@gmail.com";
            string password = "admin1@gmail.comQWE";

            string customer1UserEmail = "1@mail.ru";
            string customer1Password = "1@mail.ruQWE";

            string customer2UserEmail = "2@mail.ru";
            string customer2Password = "2@mail.ruQWE";

            string manager1UserEmail = "manager1@mail.ru";
            string manager1Password = "manager1@mail.ruQWE";

            string manager2UserEmail = "manager2@mail.ru";
            string manager2Password = "manager2@mail.ruQWE";

            string driver1UserEmail = "driver1@mail.ru";
            string driver1Password = "driver1@mail.ruQWE";

            string driver2UserEmail = "driver2@mail.ru";
            string driver2Password = "driver2@mail.ruQWE";
            
            в appsettings, при форке поменяйте строку подключения к базе на свою 
            в ApplicationDbContext сделать  тоже самое
