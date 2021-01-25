CREATE FUNCTION count_users_notes ()
RETURNS TABLE(id integer, first_name character varying, last_name character varying, 
			  count_notes integer ) AS $$
	SELECT users.*, COUNT(notes.id) 
	FROM users
	INNER JOIN notes ON notes.user_id=users.id AND notes.is_deleted=FALSE
	GROUP BY users.id, users.first_name, users.last_name
$$ LANGUAGE SQL;