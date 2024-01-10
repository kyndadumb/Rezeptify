create table ingredients (id integer primary key, name text not null, quantity float);

create table recipe (id integer primary key, instructions text);

create table recipeingredients (recipe_id integer not null, ingredient_id integer not null,
FOREIGN KEY(recipe_id) REFERENCES recipe(id)
FOREIGN KEY(ingredient_id) REFERENCES ingredients(id));