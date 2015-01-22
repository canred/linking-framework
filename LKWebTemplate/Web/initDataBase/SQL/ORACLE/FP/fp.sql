CREATE OR REPLACE FUNCTION {user}.PARENTLST (uuid IN VARCHAR2)
   RETURN VARCHAR2
IS
   lst   VARCHAR2 (4000);
BEGIN
   DECLARE
      my_uuid          VARCHAR2 (36);
      my_parent_uuid   VARCHAR2 (36);
      temp_uuid        VARCHAR2 (36);
   BEGIN
      my_uuid := uuid;
      SELECT CASE WHEN appmenu_uuid IS NULL THEN uuid ELSE appmenu_uuid END
        INTO my_parent_uuid
        FROM appmenu
       WHERE uuid = my_uuid;
      IF (my_parent_uuid = my_uuid)
      THEN
         lst := my_uuid;
      ELSE
         lst := my_uuid;
         WHILE my_parent_uuid != my_uuid
         LOOP
            lst := my_parent_uuid || ',' || lst;
            SELECT CASE
                      WHEN appmenu_uuid IS NULL THEN uuid
                      ELSE appmenu_uuid
                   END
              INTO temp_uuid
              FROM appmenu
             WHERE uuid = my_parent_uuid;
            my_uuid := my_parent_uuid;
            my_parent_uuid := temp_uuid;
         END LOOP;
      END IF;
      RETURN lst || ',';
   END;
END PARENTLST;
/