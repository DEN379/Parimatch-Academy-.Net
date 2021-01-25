SELECT * FROM insert_user('Den','Sakadel');

SELECT * FROM insert_user('Barack','Obama');

SELECT * FROM insert_user('Elon','Mask');

SELECT * FROM insert_user('Steave','Jobs');




SELECT * FROM insert_note(uuid_in('f3b8a4f1-4bc6-476d-2660-8a8dab01a9a7'),'Shop','buy milk',2);

SELECT * FROM insert_note(uuid_in('6486a9e1-87d7-6ee6-83be-83db5fef7772'),'movie','watch inception',1);

SELECT * FROM insert_note(uuid_in('39f9705a-5d97-dd0f-ed1d-938a6016302e'),'read','read news',1);

SELECT * FROM insert_note(uuid_in('96c79704-973f-7501-b930-b7d7c2bc683a'),'car','create new tesla',3);

SELECT * FROM insert_note(uuid_in(md5(random()::text || clock_timestamp()::text)::cstring),'iphone','create new iphone',4);



SELECT * FROM get_info_about_note ('6486a9e1-87d7-6ee6-83be-83db5fef7772');

SELECT * FROM get_info_about_note ('39f9705a-5d97-dd0f-ed1d-938a6016302e');




SELECT * FROM set_note_as_delete ('96c79704-973f-7501-b930-b7d7c2bc683a');

SELECT * FROM set_note_as_delete ('f3b8a4f1-4bc6-476d-2660-8a8dab01a9a7');



SELECT * FROM count_users_notes ();

SELECT * FROM count_users_notes ();




SELECT * FROM get_notes_by_user_id (1);

SELECT * FROM get_notes_by_user_id (4);