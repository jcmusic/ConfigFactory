# ConfigFactory

        /***   Instead of fetching rows, name-matching, null-checking, which is ugly, verbose, 
        *      and error-prone, use this typesafe object to get config values for your service/module
        *      
        *      Use this class in Linq statements or to make decision branching in your code much cleaner, safer, & more readable.
        *      Change UserId to clientId or customerId for your own use case.
        *      
        *      The key/value pairs are stored as strings in the database (SqlLite, UserConfig table)
        *      Note: the key (Name) string must match the name of the property in the Configuration class.
        *
        *      In this demo, a config factory, IInventoryConfigFactory, is injected into the Controller's constructor.
        *      In a real use case, you would inject it into your Inventory Service or BLL class 
        *      You then create/instantiate the config onject by calling GetCustomConfigAsync(), passing the userId. 
        *      It will return a strongly-typed object, including enums.  So handy!
        *      
        *      Create your own ConfigFactory for each services/BLL class in which you need it by deriving from the AbstractCustomConfigFactory and 
        *      creating a commensurate configuration model to populate. Again, properties should match the key name from your stored key/value pairs.
        *      
        *      Enjoy!
        * ***/
