  IF NOT EXISTS(SELECT 1 FROM AspNetRoles)
  BEGIN

        INSERT INTO AspNetRoles
        (
             [Id]
            ,[Name]
            ,[NormalizedName])
        VALUES ('0168821f-5c73-4543-beaa-804d6424a128', 'Super Admin', 'SUPER ADMIN')
            ,('5ae982a9-b2bd-412e-bf78-7a78413937be', 'Create and Edit Facts','CREATE AND EDIT FACTS')
            ,('854dc560-7046-4bf2-a118-8c14d46971ec', 'Manage Dev Club','MANAGE DEV CLUB')
  END

