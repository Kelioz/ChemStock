# 🧪 Управление складом химической промышленности

## 🚀 Начало работы

### 📥 Восстановление базы данных

1. Откройте **Microsoft SQL Server Management Studio**
2. Восстановите БД из бэкапа, находящегося в папке `BD` проекта
3. Запомните имя сервера (отображается при подключении в SSMS)

### ⚙️ Настройка подключения

Замените строку подключения в коде:

```csharp
public SqlConnection connection = new SqlConnection("Data Source=ВАШ СЕРВЕР; Initial Catalog=ChemStock; Integrated Security=True");
```

## Где `ВАШ СЕРВЕР` - имя вашего SQL-сервера (например: `localhost`, `.\SQLEXPRESS`)

#### ИЗНАЧАЛЬНО ПОЛЬЗОВАТЕЛЕЙ 2 админ(логин admin пароль admin) и кладовщик(логин 123 пароль 123)

#### Для показа таблицы с зашифрованными данными есть таблица EncryptedDataTable

#### Тестовый запрос для показа ```sql Select \* from Items Where Type = 1 ```

#### ЗАДАЧУ С РАЗГРАНИЧЕНИЕМ ПРАВ ПОЛЬЗОВАТЕЛЕЙ В БД НУЖНО СДЕЛАТЬ САМОСТОЯТЕЛЬНО В КОЛЛЕДЖЕ НА КОМПЕ ИЛИ У СЕБЯ НА ПК, ИНАЧЕ БУДУТ ОШИБКИ
