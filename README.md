````markdown
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
````
