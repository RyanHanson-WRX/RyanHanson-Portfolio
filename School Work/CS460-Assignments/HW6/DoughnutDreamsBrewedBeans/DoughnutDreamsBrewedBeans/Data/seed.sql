SET IDENTITY_INSERT [Station] ON;
INSERT INTO [Station] (Id, Name) VALUES
(1, 'Espresso Station'),
(2, 'Pastry Station'),
(3, 'Donut Station');
SET IDENTITY_INSERT [Station] OFF;

SET IDENTITY_INSERT [Store] ON;
INSERT INTO [Store] (Id, Name) VALUES
(1, 'Doughnut Dreams & Brewed Beans - Downtown');
SET IDENTITY_INSERT [Store] OFF;

SET IDENTITY_INSERT [DeliveryLocation] ON;
INSERT INTO [DeliveryLocation] (Id, Name) VALUES
(1, 'Main Counter'),
(2, 'Drive-Through'),
(3, 'Walk-in Counter');
SET IDENTITY_INSERT [DeliveryLocation] OFF;

SET IDENTITY_INSERT [Item] ON;
INSERT INTO [Item] (Id, Name, Description, Price, StationId) VALUES
(1, 'Caramel Macchiato', 'Rich espresso with caramel and steamed milk', 4.99, 1),
(2, 'Glazed Doughnut', 'Classic ring-shaped doughnut with sweet glaze', 2.49, 3),
(3, 'Fruit Explosion Bowl', 'Fresh mixed fruits with a drizzle of honey', 6.99, 2),
(4, 'Mocha Frappuccino', 'Creamy coffee-chocolate blend with whipped cream', 5.99, 1),
(5, 'Blueberry Muffin', 'Moist muffin bursting with blueberries', 3.49, 2),
(6, 'Smoothie Sensation', 'Blended fruit smoothie with yogurt and honey', 4.99, 1),
(7, 'Espresso Shot', 'A quick pick-me-up of pure espresso', 1.99, 1),
(8, 'Nutella-Filled Donut', 'Soft donut filled with luscious Nutella', 3.99, 3),
(9, 'Greek Yogurt Parfait', 'Layers of yogurt, granola, and fresh berries', 4.49, 2),
(10, 'Chai Latte', 'Spiced black tea with steamed milk and foam', 4.49, 1),
(11, 'Cinnamon Roll', 'Sweet and gooey cinnamon-swirl pastry', 3.99, 2),
(12, 'Breakfast Burrito', 'Scrambled eggs, cheese, and veggies in a tortilla', 5.49, 2),
(13, 'Iced Green Tea', 'Refreshing green tea served over ice', 3.99, 1),
(14, 'Chocolate Croissant', 'Buttery croissant with a chocolate filling', 3.49, 2),
(15, 'Veggie Wrap', 'Fresh vegetables and hummus in a whole-wheat wrap', 6.49, 2),
(16, 'Americano', 'Strong black coffee with hot water', 2.99, 1),
(17, 'Apple Fritter', 'Deep-fried pastry filled with spiced apples', 3.99, 2),
(18, 'Caprese Salad', 'Fresh tomatoes, mozzarella, and basil drizzled with balsamic', 7.49, 2),
(19, 'Decaf Latte', 'Smooth decaffeinated latte with frothy milk', 4.99, 1),
(20, 'Chocolate Milkshake', 'Rich chocolate shake topped with whipped cream', 5.49, 1),
(21, 'Latte Macaron', 'Espresso and steamed milk with a sweet macaron', 4.49, 1),
(22, 'Lemon Poppy Seed Cake', 'Zesty lemon cake with poppy seeds', 3.99, 2),
(23, 'Pomegranate Paradise', 'Pomegranate and berry smoothie with a hint of mint', 5.49, 1),
(24, 'Affogato', 'Vanilla ice cream drowned in a shot of espresso', 5.99, 1),
(25, 'Cinnamon Sugar Donut', 'Fluffy donut dusted with cinnamon and sugar', 2.99, 3),
(26, 'Mediterranean Wrap', 'Hummus, falafel, and fresh veggies in a wrap', 6.99, 2),
(27, 'Cold Brew', 'Smooth and strong cold-brewed coffee', 4.49, 1),
(28, 'Raspberry Danish', 'Flaky pastry filled with sweet raspberry jam', 3.49, 2),
(29, 'Caesar Salad', 'Crisp romaine lettuce with Caesar dressing', 5.99, 2),
(30, 'Iced Chai Latte', 'Chilled spiced tea with milk and ice', 4.99, 1),
(31, 'Chocolate Chip Scone', 'Buttery scone with chocolate chips', 3.99, 2),
(32, 'Turkey & Avocado Panini', 'Turkey, avocado, and Swiss cheese on grilled ciabatta', 6.49, 2),
(33, 'Hazelnut Latte', 'Rich latte with a nutty twist of hazelnut', 4.99, 1),
(34, 'S''mores Donut', 'Donut filled with marshmallow and chocolate', 3.49, 3),
(35, 'Quinoa Salad', 'Quinoa, mixed vegetables, and lemon vinaigrette', 6.99, 2),
(36, 'Hot Chocolate', 'Creamy and indulgent hot chocolate', 4.49, 1),
(37, 'Almond Croissant', 'Flaky croissant with a delightful almond filling', 3.99, 2),
(38, 'BBQ Pulled Pork Sandwich', 'Slow-cooked pulled pork with barbecue sauce', 6.99, 2),
(39, 'Iced Matcha Latte', 'Iced green tea latte with a hint of matcha', 4.99, 1),
(40, 'Red Velvet Cupcake', 'Decadent red velvet cupcake with cream cheese frosting', 3.49, 2);
SET IDENTITY_INSERT [Item] OFF;
