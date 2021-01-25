CREATE FUNCTION get_info_about_note (id UUID)
RETURNS TABLE(id uuid, header character varying, body character varying, is_deleted boolean, 
			  user_id integer, modified_at timestamp with time zone,
			  first_name character varying, last_name character varying) AS $$

SELECT notes.*, users.first_name, users.last_name
	FROM public.notes FULL JOIN public.users ON notes.user_id=users.id
	 WHERE notes.id=get_info_about_note.id;
$$ LANGUAGE SQL;