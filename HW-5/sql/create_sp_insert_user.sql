CREATE FUNCTION insert_user (first_name character varying, last_name character varying)
RETURNS TABLE(id integer) AS $$
	INSERT INTO public.users(first_name, last_name)
	VALUES (insert_user.first_name, insert_user.last_name);
	SELECT id FROM public.users WHERE first_name=insert_user.first_name AND last_name=insert_user.last_name;
$$ LANGUAGE SQL;

