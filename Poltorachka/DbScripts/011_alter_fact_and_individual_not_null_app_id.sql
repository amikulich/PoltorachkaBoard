IF EXISTS (SELECT 1 FROM fact WHERE app_id IS NULL)
BEGIN

    UPDATE fact
    SET app_id = 1
    WHERE app_id IS NULL

END

IF EXISTS (SELECT 1 FROM individual WHERE app_id IS NULL)
BEGIN

    UPDATE individual
    SET app_id = 1
    WHERE app_id IS NULL

END

ALTER TABLE fact ALTER COLUMN app_id INT NOT NULL
ALTER TABLE individual ALTER COLUMN app_id INT NOT NULL

