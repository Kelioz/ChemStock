Вот отформатированный `README.MD` файл с красивым оформлением для вашего проекта:

````markdown
# 🧪 Управление складом химической промышленности

![Chemical Warehouse](https://img.icons8.com/color/96/000000/chemical-weapon.png)

Профессиональное приложение для управления складом химических веществ с полным набором CRUD-операций, отчетностью и безопасным доступом.

---

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
````
