@StudentsController
Feature: StudentsController

Scenario: 0100 I try to GET student list. When I try to get list, it should return related status code
	Given <Employee Object> model
	 When I call the get all students action
	 Then it should return <Expected Status Code>
	    * raw response message should contain <Expected Response Message>
Examples:
	| Index | Employee Object         | Expected Status Code | Expected Response Message |
	| 01    | ''                      | 401                  | 'Invalid ApiKey'          |
	| 02    | '{"sendApiKey": false}' | 401                  | 'Invalid ApiKey'          |
	| 03    | '{"sendApiKey": true}'  | 200                  | '[{"id":'                 |

Scenario: 0200 I try to GET one student by Id. When I try to get by Id, it should return related status code
	Given <Employee Object> model
	 When I call the get by Id student action
	 Then it should return <Expected Status Code>
	    * raw response message should contain <Expected Response Message>
Examples:
	| Index | Employee Object                    | Expected Status Code | Expected Response Message |
	| 01    | ''                                 | 401                  | 'Invalid ApiKey'          |
	| 02    | '{"sendApiKey": false}'            | 401                  | 'Invalid ApiKey'          |
	| 03    | '{"sendApiKey": true, "value": 3}' | 200                  | '{"id":3'                 |
	| 04    | '{"sendApiKey": true, "value": 4}' | 200                  | '{"id":4'                 |

Scenario: 0300 I try to CREATE student. When I try to create, it should return related status code
	Given <Employee Object> model
	 When I call the create student action
	 Then it should return <Expected Status Code>
	    * raw response message should contain <Expected Response Message>
Examples:
	| Index | Employee Object                                                                                                                                                  | Expected Status Code | Expected Response Message                                                       |
	| 01    | '{"sendApiKey": true, "value": null}'                                                                                                                            | 415                  | 'Unsupported Media Type'                                                        |
	| 02    | '{"sendApiKey": true, "value": {"id":0,"name":null,"birthDate":"2001-08-09T00:00:00+03:00","birthPlace":"NearSomewhere","image":null}}'                          | 400                  | 'The Name field is required'                                                    |
	| 03    | '{"sendApiKey": true, "value": {"id":0,"name":"Ab","birthDate":"2001-08-09T00:00:00+03:00","birthPlace":"NearSomewhere","image":null}}'                          | 400                  | "The length of 'Name' must be at least 3 characters. You entered 2 characters." |
	| 04    | '{"sendApiKey": true, "value": {"id":0,"name":"Dex Terr","birthDate":"2001-08-09T00:00:00+03:00","birthPlace":"NearSomewhere","image":null}}'                    | 400                  | 'The EmailAddress field is required.'                                           |
	| 05    | '{"sendApiKey": true, "value": {"id":0,"name":"Dex Terr","birthDate":"2001-08-09T00:00:00+03:00","birthPlace":"NearSomewhere","emailAddress":"sdfsfsdf"}}'       | 400                  | "'Email Address' is not a valid email address."                                 |
	| 06    | '{"sendApiKey": true, "value": {"id":0,"name":"Dex Terr","birthDate":"2001-08-09T00:00:00+03:00","birthPlace":"NearSomewhere","emailAddress":"test5@test.com"}}' | 201                  | '"name":"Dex Terr","emailAddress":"test5@test.com"'                             |

Scenario: 0400 I try to UPDATE student. When I try to update, it should return related status code
	Given <Employee Object> model
	 When I call the update student action
	 Then it should return <Expected Status Code>
	    * raw response message should contain <Expected Response Message>
Examples:
	| Index | Employee Object                                                                                                                                                  | Expected Status Code | Expected Response Message                                                       |
	| 01    | '{"sendApiKey": true, "value": null}'                                                                                                                            | 415                  | 'Unsupported Media Type'                                                        |
	| 02    | '{"sendApiKey": true, "value": {"id":0,"name":null,"birthDate":"2001-08-09T00:00:00+03:00","birthPlace":"NearSomewhere","image":null}}'                          | 400                  | 'The Name field is required'                                                    |
	| 03    | '{"sendApiKey": true, "value": {"id":0,"name":"Ab","birthDate":"2001-08-09T00:00:00+03:00","birthPlace":"NearSomewhere","image":null}}'                          | 400                  | "The length of 'Name' must be at least 3 characters. You entered 2 characters." |
	| 04    | '{"sendApiKey": true, "value": {"id":0,"name":"Dex Terr","birthDate":"2001-08-09T00:00:00+03:00","birthPlace":"NearSomewhere","image":null}}'                    | 400                  | 'The EmailAddress field is required.'                                           |
	| 05    | '{"sendApiKey": true, "value": {"id":0,"name":"Dex Terr","birthDate":"2001-08-09T00:00:00+03:00","birthPlace":"NearSomewhere","emailAddress":"sdfsfsdf"}}'       | 400                  | "'Email Address' is not a valid email address."                                 |
	| 06    | '{"sendApiKey": true, "value": {"id":0,"name":"Dex Terr","birthDate":"2001-08-09T00:00:00+03:00","birthPlace":"NearSomewhere","emailAddress":"test5@test.com"}}' | 404                  | 'Not Found'                                                                     |
	| 07    | '{"sendApiKey": true, "value": {"id":3,"name":"Dex Terr","birthDate":"2001-08-09T00:00:00+03:00","birthPlace":"NearSomewhere","emailAddress":"test5@test.com"}}' | 200                  | '"name":"Dex Terr","emailAddress":"test5@test.com"'                             |

Scenario: 0500 I try to DELETE one student by Id. When I try to delete, it should return related status code
	Given <Employee Object> model
	 When I call the delete student action
	 Then it should return <Expected Status Code>
	    * raw response message should contain <Expected Response Message>
Examples:
	| Index | Employee Object                       | Expected Status Code | Expected Response Message |
	| 01    | '{"sendApiKey": true, "value": null}' | 405                  | ''                        |
	| 02    | '{"sendApiKey": true, "value": -1}'   | 404                  | 'Not Found'               |
	| 03    | '{"sendApiKey": true, "value": 3}'    | 200                  | ''                        |
	| 04    | '{"sendApiKey": true, "value": 4}'    | 200                  | ''                        |