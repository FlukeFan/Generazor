<h1>Generazor docs</h1>

Template based code generation for .NET Core using the Razor SDK.

A (very) lightweight replacement for something like T4 templates in .NET Core.

The Razor SDK can be used to add Razor templates to any .NET assembly.  These get compiled into a separate assembly with the extension .Views.dll.  Generazor is a tiny library to allow you to pass an arbitrary model to the views, and render them to a string.  The advantage of using the Razor SDK is that Visual Studio will give design-time intellisense making the templates easier to maintain.

<i>
Note, since Generazor allows generation to a string outside of MVC, the intellisense (and generated view) will have access to other properties than just the @Model (e.g., @Component, @Html, @Json, @Url).  The other properties are genearated, but are &lt;null&gt; at runtime.  In practice, it's easy to ingore these.
</i>

<ul>
    <li><a href="quickstart.html">Quickstart</a></li>
</ul>
