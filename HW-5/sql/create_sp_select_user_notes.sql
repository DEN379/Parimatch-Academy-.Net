
CREATE FUNCTION get_notes_by_user_id (id integer)
RETURNS TABLE(id_note uuid, header character varying, body character varying, is_deleted boolean, 
			  user_id integer, modified_at timestamp with time zone ) AS $$
	SELECT id, header, body, is_deleted, user_id, modified_at
	FROM public.notes WHERE user_id=get_notes_by_user_id.id AND is_deleted=FALSE;
$$ LANGUAGE SQL;