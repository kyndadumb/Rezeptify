CREATE TABLE "ingredients" (
	"id" INTEGER NOT NULL,
	"name" TEXT NOT NULL,
	"quantity" REAL NULL,
	"unit" VARCHAR(50) NULL,
	PRIMARY KEY ("id")
);


CREATE TABLE "recipe" (
	"id" INTEGER NOT NULL,
	"instructions" TEXT NULL,
	"name" TEXT NULL,
	PRIMARY KEY ("id")
);


CREATE TABLE "recipeingredients" (
	"recipe_id" INTEGER NOT NULL,
	"ingredient_id" INTEGER NOT NULL,
	CONSTRAINT "0" FOREIGN KEY ("ingredient_id") REFERENCES "ingredients" ("id") ON UPDATE NO ACTION ON DELETE NO ACTION,
	CONSTRAINT "1" FOREIGN KEY ("recipe_id") REFERENCES "recipe" ("id") ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE "eancodes" (
	"id" INTEGER NOT NULL,
	"eancode" VARCHAR(255) NOT NULL,
	"ingredient_id" INTEGER NOT NULL,
	CONSTRAINT "0" FOREIGN KEY ("ingredient_id") REFERENCES "ingredients" ("id") ON UPDATE NO ACTION ON DELETE NO ACTION
);
