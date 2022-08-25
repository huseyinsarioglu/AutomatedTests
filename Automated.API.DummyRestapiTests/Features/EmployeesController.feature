Feature: EmployeesController

Scenario: 0100 I try to GET employee list. When I try get list, it should return Ok
	 When I call the get all employees action
	 Then it should return 200
	    * raw response message should contain '"status":"success"'

Scenario: 0200 I try to GET employee by Id. When I try get employee, it should return related status code
	Given model '<Id>'
	 When I call the get employee by id action
	 Then it should return <Expected Status Code>
	    * raw response message should contain <Expected Response Message>
Examples:
	| Id  | Expected Status Code | Expected Response Message             |
	| -1  | 200                  | '"status":"success","data":null'      |
	| 2   | 200                  | '{"status":"success","data":{"id":2'  |
	| 15  | 200                  | '{"status":"success","data":{"id":15' |
	| 332 | 404                  | 'Not found'                           |

Scenario: 0300 I try to CREATE employee. When I try create employee, it should return related status code
	Given <Employee Object> model
	 When I call the Create employee action
	 Then it should return <Expected Status Code>
	    * raw response message should contain <Expected Response Message>
Examples:
	| Index | Employee Object                           | Expected Status Code | Expected Response Message       |
	| 01    | ''                                        | 400                  | ''                              |
	| 02    | '{"name":"",    "salary":0,"age":0}'      | 400                  | ''                              |
	| 03    | '{"name":"test","salary":0,"age":0}'      | 400                  | ''                              |
	| 04    | '{"name":"test","salary":12230,"age":0}'  | 400                  | ''                              |
	| 05    | '{"name":"test","salary":12230,"age":30}' | 200                  | '{"status":"success","data":{"' |

Scenario: 0400 I try to UPDATE employee. When I try update employee, it should return related status code
	Given <Employee Object> model
	 When I call the Update employee action
	 Then it should return <Expected Status Code>
	    * raw response message should contain <Expected Response Message>
Examples:
	| Index | Employee Object                                     | Expected Status Code | Expected Response Message                        |
	| 01    | ''                                                  | 405                  | 'The PUT method is not supported for this route' |
	| 02    | '{"id": 0,  "name":"",    "salary":0,    "age":0}'  | 404                  | 'Not found'                                      |
	| 03    | '{"id": 10, "name":"",    "salary":0,    "age":0}'  | 400                  | 'Bad Request. Name is invalid'                   |
	| 04    | '{"id": 10, "name":"test","salary":0,    "age":0}'  | 400                  | 'Bad Request. Salary is invalid'                 |
	| 05    | '{"id": 10, "name":"test","salary":12000,"age":0}'  | 400                  | 'Bad Request. Age is invalid'                    |
	| 06    | '{"id": 10, "name":"test","salary":12230,"age":30}' | 200                  | '{"status":"success","data":{'                   |

Scenario: 0500 I try to DELETE employee by Id. When I try delete employee, it should return related status code
	Given model '<Id>'
	 When I call the delete employee by id action
	 Then it should return <Expected Status Code>
	    * raw response message should contain <Expected Response Message>
Examples:
	| Id  | Expected Status Code | Expected Response Message                                                            |
	| -1  | 404                  | 'Not found'                                                                          |
	| 332 | 404                  | 'Not found'                                                                          |
	| 2   | 200                  | '{"status":"success","data":"2","message":"Successfully! Record has been deleted"}'  |
	| 15  | 200                  | '{"status":"success","data":"15","message":"Successfully! Record has been deleted"}' |
