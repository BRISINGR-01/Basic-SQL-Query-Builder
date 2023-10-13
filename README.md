# SQL Query Builder
## Description
### Implements the [Builder pattern](https://refactoring.guru/design-patterns/builder)
Chained methods create an sql query  
ex: `sql.Select.All.Where("<user_table.id>").Equals(<id>).First<User>();`

## Select
### First step - columns
Choose between getting all columns or only certain few:  
`...Select.All...`   
or  
`...Select.OnlyColumns("<table.id>", "<table.name>")...`

### Second step - join/where/SelectFinish

#### Join
`...All.Join("<second_table>").OnColumns("<first_table.column>","<scond_table.column>")...`   

translates to:  

`SELECT * FROM first_table INNER JOIN scond_table ON first_table.column = scond_table.column`

can specify the type of join

#### Where

`...All.Where("table.column").Equals(valueOfAnyType)...`
The value should either be a supported type or a type with `.ToString` method

##### Supported types
`Guid`, `DateTime`, `Enum`

#### FinishSelect
After a `Where` clause you have to select the `FinishSelect` option
`...All.Where("table.column").Equals(valueOfAnyType).FinishSelect...`

Or use it directly after a `Join` or after specifying the columns
##### To make the query ordered
`...All.OrderBy("<table.column>", isAscending: true)...`
##### To get a nullable single value
`...All.First<Type>();`
##### To get a list of values
`...All.Get<Type>();`
##### To get the count of selected columns
`...All.Count;`

translates to   

`SELECT COUNT(*)`

## Insert / Update / Delete

```
...
.Insert
.Set("<table.column>", value)
...
.Execute()
```
```
...
.Update
.Set("<table.column>", value)
...
.Where("<table.column>").Equals(value)
.Execute()
```
```
...
.Delete
.Where("<table.column>").Equals(value)
.Execute()
```





## EntityBuilder

### Implements the [Factory pattern](https://refactoring.guru/design-patterns/factory-method)

### Description
Instantiates classes.  
When the `SqlQueryBuilder` returns a `List` or nullable instance of a class, the objects are created using the class that inherits from `IEntityBuilder`. 

### Used
#### DataLayer.UserRepository.cs
```
public User? GetOneUser() {
    return sql.Select.All.First<User>()
}
public List<User> GetAll() {
    retunr sql.Select.All.Get<User>()
}
```

#### SqlQueryBuilder.Select.SelectFinish.cs
```
public T? First<T>()
{
    T? value = default;
    ...
    if (reader.Read()) value = command.EntityBuilder.Create<T>(reader);
    ...
    return value;
}
```
(`reader` implements `IDbDataReader`)  

### Implementation
#### EntityBuilder.cs

```
public class EntityBuilder: IEntityBuilder {
    public T Create<T>(IDbDataReader reader)
    {
        return typeof(T) switch
        {
            Type UserType when UserType == typeof(User) => (T)(object)UserConverter(reader),
            _ => throw new Exception($"A converter for {typeof(T).Name} is not implemented"),
        };
        or
        if (typeof(T) == typeof(User))
        {
            return (T)(object)UserConverter(reader);
        }
    }
}
private User UserConverter(IDbDataReader reader)
{
    return new User(
        id: reader.GetId("<user_table.id>")
        name: reader.GetString("<user_table.name>")
    );
}
```

## EnumToStringConverter

### Description

When an enum value is fed to the sql query builder (in an `Insert` or `Update` for example) the value has to be converted to a string. By default enums can be simlply converted to a lowercase string, but if more control is needed this class can be used.

ex: `UserRole.PrimaryCaretaker` => `"primary_caretaker"`

### Used

#### UserRole.cs

```
enum UserRole {
    PrimaryCaretaker,
    SecondaryCaretaker,
    None
} 
```

#### UserRepository.cs

```
...
sql.Insert.Set("<user_table.role>", UserRole.PrimaryCaretaker)
...
```

#### SqlQueryBuilder.Insert.cs

```
public Insert Set(string column, object? value)
{
    ...
    string stringValue;
    ...
    else if (value is Enum) {
        stringValue = enumConverter.Convert(@enum);
    }
    ...
}
```

#### Implementation

```
public class EnumConverter : IEnumToStringConverter
{
    public string Convert(Enum @enum)
    {
        switch (@enum) {
            case UserRole.PrimaryCaretaker:
                return "primary_caretaker";
            default:
                return @enum.ToString().ToLower();
        }
    }
}
