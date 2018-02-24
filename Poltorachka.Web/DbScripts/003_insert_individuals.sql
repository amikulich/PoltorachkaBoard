IF NOT EXISTS (SELECT 1 FROM individual)
BEGIN
	INSERT INTO [dbo].[individual]
			   ([name])
		 VALUES
		        ('Artem Mikulich')
			   ,('Andrey Ravkov')
			   ,('Sergey Morgachev')
			   ,('Alexey Rozdyalovsky')
			   ,('Rita Khodarkevich')
			   ,('Irina Makarevich')
			   ,('Pavel Yuruts')
			   ,('Shamil Salsanov')
			   ,('Dmitry Stashevsky')
			   ,('Stan Altukhov')
END