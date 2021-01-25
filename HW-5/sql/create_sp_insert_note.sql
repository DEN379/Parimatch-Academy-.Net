CREATE FUNCTION insert_note (id UUID, header character varying, body character varying,  user_id integer)
RETURNS TABLE(modified_at timestamp with time zone) AS $$

INSERT INTO public.notes(id, header, body, is_deleted, user_id)
	VALUES (insert_note.id, insert_note.header, insert_note.body, FALSE, insert_note.user_id);
SELECT modified_at FROM public.notes WHERE id=insert_note.id;
$$ LANGUAGE SQL;