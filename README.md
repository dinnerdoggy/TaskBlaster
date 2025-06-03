# TaskBlaster 🧨

## Overview
TaskBlaster is a productivity-focused API designed to help users create, organize, and complete tasks called “Duties.” It supports categories, comments, and reusable resources — all built on a C# ASP.NET Core backend with PostgreSQL and Firebase integration.

This project is ideal as the backend foundation for a to-do app or goal tracking system.

## About the User
- **Ideal User**: Perfect for busy professionals or students who want to organize their personal, work, or school tasks with flexible tagging, categorization, and notes.

## Features
- **Duties CRUD**: Create, view, update, and delete duties (to-dos).
- **Categorization**: Group duties under custom user-defined categories.
- **Priority Levels**: Assign duties a priority ("High", "Medium", "Low").
- **Resource Linking**: Connect reusable resources (like tools, docs) to multiple duties via a many-to-many relationship.
- **Commenting**: Leave notes or collaborate with comments on duties.
- **Toggle Completion**: Mark duties as complete/incomplete with a single toggle.
- **User-Specific Data**: Uses Firebase UID to ensure secure, user-isolated data access.
- **Full CRUD on all four entities.

## Code Snippet
Here’s how to POST a new Duty with linked resources:
```json
POST /api/duties
Content-Type: application/json

{
  "title": "Write Unit Tests",
  "description": "Write tests for the ResourceService",
  "isCompleted": false,
  "categoryId": 2,
  "priority": "High",
  "resources": [
    { "id": 8 },
    { "id": 2 }
  ]
}
```

## Contributors
[Casey Cunningham](https://github.com/dinnerdoggy)
