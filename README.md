# TodoApi

TodoApi הוא פרויקט API לניהול רשימת משימות (ToDo) שנבנה באמצעות ASP.NET Core ו-Entity Framework Core. הפרויקט כולל פונקציונליות CRUD (Create, Read, Update, Delete) לניהול משימות, ומאפשר אינטגרציה עם מסד נתונים MySQL. הפרויקט כולל גם תמיכה ב-Swagger לתיעוד ה-API וב-Docker לצורך פריסה קלה.

## תכונות עיקריות

- **פונקציונליות CRUD מלאה**: יצירה, קריאה, עדכון ומחיקה של משימות.
- **אינטגרציה עם MySQL**: שימוש ב-Entity Framework Core לחיבור למסד נתונים MySQL.
- **תמיכה ב-Swagger**: תיעוד אוטומטי של ה-API באמצעות Swagger.
- **פריסה באמצעות Docker**: קובץ Dockerfile לפריסה קלה ומהירה של הפרויקט.

## דרישות מערכת

- .NET 7.0 SDK
- MySQL Server
- Docker (לא חובה, אך מומלץ)

## התקנה והפעלה

1. **שכפול הריפוזיטורי**:
    ```sh
    git clone https://github.com/sari-Yona/TodoApi.git
    cd TodoApi
    ```

2. **הגדרת משתני סביבה**:
    צור קובץ `.env` בתיקיית הפרויקט הראשית והוסף את משתנה הסביבה הבא:
    ```env
    CONNECTION_STRING=your_connection_string_here
    ```

3. **שימוש ב-Docker**:
    אם ברצונך להשתמש ב-Docker, הרץ את הפקודות הבאות:
    ```sh
    docker build -t todoapi .
    docker run -p 80:80 todoapi
    ```

4. **הפעלה מקומית**:
    אם ברצונך להפעיל את הפרויקט מקומית, הרץ את הפקודות הבאות:
    ```sh
    dotnet restore
    dotnet build
    dotnet run
    ```

## שימוש ב-API

### קבלת כל המשימות
```http
GET /api/items
