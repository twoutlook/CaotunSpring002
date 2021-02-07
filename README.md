"# Lab0130" 
## 希望將 Adapters 單獨成為 NuGet Package
- 能做為 package 先上傳
- 能按標準 package 引用, 但是報錯
- 之前有單獨用 ddl , 可以

## 希望將 Blazor 的 razor components 單獨成為 NuGet Package
- 能做為 package 先上傳
- 還沒試能不能按標準 package 引用
- 以 project 還 NS3引用, 一直有錯.
  - 之前讓 NS2 引用曾是OK.

## 要不要將  Dynamic LINQ  Object 的方式封裝在 Adapter 裡
- 目前是改寫在各自 Blazor 頁面的 razor component 裡
- Adapter 可以不具有任何泛型
- 是不是再封裝回去?

## 目前 自動生成 Table 的  razor component 是另外一個核心
- 可以再強化不同類型的格式.

## 希望將數據庫的 Table/View 都自動生成 Blazor 頁面
- 目前工具是 Console
- 是不是就轉到 Blazor Server project?


## Local NuGet
```
D:\nuget_repo>nuget add D:\2021\Lab\Lab0130\Lab0130\CaotunSpringC000Components\bin\Debug\CaotunSpring.C000.1.0.4.nupkg -source d:\nuget_repo
Installing CaotunSpring.C000 1.0.4.
Successfully added package 'D:\2021\Lab\Lab0130\Lab0130\CaotunSpringC000Components\bin\Debug\CaotunSpring.C000.1.0.4.nupkg' to feed 'd:\nuget_repo'.

```
"# CS" 
